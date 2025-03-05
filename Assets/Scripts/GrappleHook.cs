using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GrappleHook : MonoBehaviour
{
    [Header("References")]
    public Transform grappleSpawnPoint;
    public GameObject grappleArm;
    public Camera grappleCamera;
    public Camera playerCamera;
    public Image crosshair;
    public AudioClip grappleHitSound;
    public float grappleWidth = 0.2f;

    [Header("Settings")]
    public float extendSpeed = 5.0f;
    public float maxGrappleDistance = 20f;
    public float minGrappleDistance = 1f;
    public float adsSensitivity = 2.0f;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    private BoxCollider grappleCollider;
    private float currentLength = 0.1f;

    private bool isAiming = false;
    private bool isGrappling = false;
    private AudioSource audioSource;

    public UnityEvent onGrappleAttached;

    void Start()
    {
        grappleArm.SetActive(false);
        grappleCamera.gameObject.SetActive(false);
        crosshair.enabled = false;
        InitializeGrapple();

        // Ensure Unity event is initialized
        if (onGrappleAttached == null)
        {
            onGrappleAttached = new UnityEvent();
        }

        onGrappleAttached.AddListener(OnGrappleSuccess);
    }


    void Update()
    {
        HandleADS();
        HandleGrappleControl();

        if (isAiming)
        {
            AimWithMouse();
        }

        UpdateGrappleMesh();

        // Continuously check for collisions with objects
        DetectGrappleCollision();
    }


    private void HandleADS()
    {
        if (Input.GetMouseButtonDown(1))
            EnterADSMode();
        else if (Input.GetMouseButtonUp(1))
            ExitADSMode();
    }

    private void HandleGrappleControl()
    {
        if (isAiming)
        {
            if (Input.GetKey(KeyCode.Q)) ExtendGrapple();
            if (Input.GetKey(KeyCode.E)) RetractGrapple();
            if (Input.GetMouseButtonDown(0)) ShootProjectile();  // Shoot with left-click
            if (Input.GetKeyDown(KeyCode.LeftControl)) ReleaseGrapple();
        }
    }


    private void EnterADSMode()
    {
        isAiming = true;
        playerCamera.gameObject.SetActive(false);
        grappleCamera.gameObject.SetActive(true);
        crosshair.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ExitADSMode()
    {
        isAiming = false;
        playerCamera.gameObject.SetActive(true);
        grappleCamera.gameObject.SetActive(false);
        crosshair.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void AimWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * adsSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * adsSensitivity;

        Vector3 rotation = grappleArm.transform.localEulerAngles;
        rotation.y += mouseX;
        rotation.x -= mouseY;
        grappleArm.transform.localRotation = Quaternion.Euler(rotation);
    }

    private void InitializeGrapple()
    {
        GameObject grappleObject = new("Grapple");
        grappleObject.transform.SetParent(grappleSpawnPoint);
        grappleObject.transform.localPosition = Vector3.zero;

        meshFilter = grappleObject.AddComponent<MeshFilter>();
        meshRenderer = grappleObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        meshRenderer.material.color = Color.red;

        mesh = new Mesh();
        meshFilter.mesh = mesh;
        UpdateGrappleMesh();

        // Attach and store collider reference
        grappleCollider = grappleObject.AddComponent<BoxCollider>();
        grappleCollider.material = CreatePhysicsMaterial();
    }


    private PhysicsMaterial CreatePhysicsMaterial()
    {
        PhysicsMaterial newMaterial = new PhysicsMaterial("GrappleMaterial")
        {
            dynamicFriction = 0.4f,  // Friction when in motion
            staticFriction = 0.6f,   // Friction when stationary
            bounciness = 0.2f,       // Controls how bouncy the grapple is
            frictionCombine = PhysicsMaterialCombine.Average,
            bounceCombine = PhysicsMaterialCombine.Maximum
        };

        return newMaterial;
    }
    // Create the grapple box mesh
    private void UpdateGrappleMesh()
    {
        float halfWidth = grappleWidth / 2f;
        float halfHeight = grappleWidth / 2f;
        float length = currentLength;

        Vector3[] vertices = new Vector3[]
        {
        new Vector3(-halfWidth, -halfHeight, 0),
        new Vector3(halfWidth, -halfHeight, 0),
        new Vector3(-halfWidth, halfHeight, 0),
        new Vector3(halfWidth, halfHeight, 0),
        new Vector3(-halfWidth, -halfHeight, length),
        new Vector3(halfWidth, -halfHeight, length),
        new Vector3(-halfWidth, halfHeight, length),
        new Vector3(halfWidth, halfHeight, length)
        };

        int[] triangles = new int[]
        {
        0, 2, 1, 1, 2, 3,
        4, 5, 6, 5, 7, 6,
        0, 4, 2, 2, 4, 6,
        1, 3, 5, 3, 7, 5,
        0, 1, 4, 1, 5, 4,
        2, 6, 3, 3, 6, 7
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Ensure the collider exists before modifying it
        if (grappleCollider != null)
        {
            grappleCollider.size = new Vector3(grappleWidth, grappleWidth, currentLength);
            grappleCollider.center = new Vector3(0, 0, currentLength / 2);
        }
        else
        {
            //Debug.LogError("BoxCollider is missing on the grapple object.");
        }
    }


    private void ExtendGrapple()
    {
        if (currentLength < maxGrappleDistance)
        {
            currentLength += extendSpeed * Time.deltaTime;
        }
    }

    private void RetractGrapple()
    {
        if (currentLength > minGrappleDistance)
        {
            currentLength -= extendSpeed * Time.deltaTime;
        }
    }

    private void DetectGrappleCollision()
    {
        // Perform raycasting from grapple spawn point
        Debug.DrawRay(grappleSpawnPoint.position, grappleSpawnPoint.forward * currentLength, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(grappleSpawnPoint.position, grappleSpawnPoint.forward, out hit, currentLength))
        {
            //Debug.Log("Grapple hit: " + hit.collider.name);

            if (hit.collider.CompareTag("GrapplePoint") && !isGrappling)
            {
                isGrappling = true;
                Debug.Log("Grapple attached to " + hit.collider.name);

                // Play the sound effect when grapple attaches
                if (grappleHitSound != null)
                {
                    audioSource.PlayOneShot(grappleHitSound);
                }
                else
                {
                    Debug.LogWarning("No grapple hit sound assigned!");
                }

                // Trigger event
                if (onGrappleAttached != null)
                {
                    onGrappleAttached.Invoke();
                }
            }
        }
    }

    void OnGrappleSuccess()
    {
        Debug.Log("Grapple successfully attached! Event triggered.");
    }

    private void ReleaseGrapple()
    {
        isGrappling = false;
        currentLength = minGrappleDistance;
        Debug.Log("Grapple Released");
    }

    private void ShootProjectile()
    {
        GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        projectile.name = "GrappleProjectile";

        // Set the position at the arm tip
        projectile.transform.position = grappleSpawnPoint.position;

        // Set sphere size
        projectile.transform.localScale = Vector3.one * 0.5f;  // Adjust size if needed

        // Add a Rigidbody component
        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.mass = 1.0f;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Add Physics Material
        SphereCollider collider = projectile.GetComponent<SphereCollider>();
        collider.material = CreatePhysicsMaterial();
        collider.isTrigger = true;

        // Apply forward force to launch the projectile
        float shootForce = 20.0f;
        rb.AddForce(grappleSpawnPoint.forward * shootForce, ForceMode.Impulse);

        // Destroy the projectile after 5 seconds to prevent clutter
        Destroy(projectile, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArmPickup"))
        {
            ActivateGrappleArm();
            Destroy(other.gameObject);
        }
    }

    private void ActivateGrappleArm()
    {
        grappleArm.SetActive(true);
        Debug.Log("Grapple Arm Activated!");
    }

}
