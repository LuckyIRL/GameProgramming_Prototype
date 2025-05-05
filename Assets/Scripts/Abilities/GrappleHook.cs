using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GrappleHook : MonoBehaviour
{
    [Header("References")]
    public Transform grappleOrigin;
    private Vector3 grapplePoint;
    public GameObject grappleArm;
    public Camera grappleCamera;
    public Camera playerCamera;
    public Image crosshair;
    public AudioClip grappleHitSound;
    public float grappleWidth = 0.2f;
    private LineRenderer lineRenderer;

    [Header("Trail Effect")]
    public GameObject projectileTrailPrefab;

    [Header("Materials")]
    public Material projectileMaterial;
    public Material grappleMaterial;


    [Header("Settings")]
    public Transform firePoint;
    public float grappleSpeed = 20f;
    public float maxGrappleDistance = 15f;
    public float minGrappleDistance = 0.1f;
    public LayerMask grappleLayer;
    public float adsSensitivity = 2.0f;

    private bool isAiming = false;
    private bool isGrappling = false;
    private bool shootMode = false;

    private AudioSource audioSource;
    public UnityEvent onGrappleAttached;

    void Start()
    {
        grappleArm.SetActive(false);
        grappleCamera.gameObject.SetActive(false);
        crosshair.enabled = false;
        InitializeGrapple();

        if (onGrappleAttached == null)
            onGrappleAttached = new UnityEvent();

        onGrappleAttached.AddListener(OnGrappleSuccess);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        HandleADS();

        if (isAiming)
            AimWithMouse();

        if (Input.GetKeyDown(KeyCode.G))
        {
            shootMode = !shootMode;
            Debug.Log("Switched to " + (shootMode ? "Shoot Mode" : "Grapple Mode"));
        }

        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            if (!shootMode)
                FireGrapple();
            else
                Shoot();
        }

        // Release grapple when left mouse button is released
        if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            ReleaseGrapple();
        }

        if (isGrappling)
            UpdateGrapple();

        UpdateGrappleLine();
    }

    void FireGrapple()
    {
        Ray ray = grappleCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;
            Debug.Log("Grappling to " + grapplePoint);
        }
    }

    void UpdateGrapple()
    {
        Vector3 direction = grapplePoint - transform.position;
        transform.position += direction.normalized * grappleSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, grapplePoint) < 0.5f)
            ReleaseGrapple();
    }

    void UpdateGrappleLine()
    {
        if (isGrappling)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot Mode Active - Implement shooting here");

        GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        projectile.name = "GrappleProjectile";
        projectile.transform.position = grappleOrigin.position;
        projectile.transform.localScale = Vector3.one * 0.5f;

        if (projectileMaterial != null)
        {
            Renderer rend = projectile.GetComponent<Renderer>();
            rend.material = projectileMaterial;
        }

        Rigidbody rb = projectile.AddComponent<Rigidbody>();
        rb.mass = 1.0f;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        SphereCollider collider = projectile.GetComponent<SphereCollider>();
        collider.material = CreatePhysicsMaterial();
        collider.isTrigger = true;

        float shootForce = 20.0f;
        rb.AddForce(grappleOrigin.forward * shootForce, ForceMode.Impulse);

        // Attach the trail effect
        if (projectileTrailPrefab != null)
        {
            GameObject trail = Instantiate(projectileTrailPrefab, projectile.transform.position, Quaternion.identity);
            trail.transform.SetParent(projectile.transform); // so it follows the projectile
        }

        Destroy(projectile, 5f); // auto-cleanup
    }


    private void HandleADS()
    {
        if (Input.GetMouseButtonDown(1))
            EnterADSMode();
        else if (Input.GetMouseButtonUp(1))
            ExitADSMode();
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
        GameObject grappleObject = new("GrappleLine");
        grappleObject.transform.SetParent(grappleOrigin);
        grappleObject.transform.localPosition = Vector3.zero;

        lineRenderer = grappleObject.AddComponent<LineRenderer>();
        lineRenderer.material = grappleMaterial;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;

        lineRenderer.enabled = false;
    }

    private PhysicsMaterial CreatePhysicsMaterial()
    {
        PhysicsMaterial newMaterial = new PhysicsMaterial("GrappleMaterial")
        {
            dynamicFriction = 0.4f,
            staticFriction = 0.6f,
            bounciness = 0.2f,
            frictionCombine = PhysicsMaterialCombine.Average,
            bounceCombine = PhysicsMaterialCombine.Maximum
        };

        return newMaterial;
    }

    private void OnGrappleSuccess()
    {
        Debug.Log("Grapple successfully attached! Event triggered.");

        if (grappleHitSound != null)
        {
            audioSource.PlayOneShot(grappleHitSound);
        }
    }

    private void ReleaseGrapple()
    {
        isGrappling = false;
        Debug.Log("Grapple Released");
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
        //Debug.Log("Grapple Arm Activated!");
    }
}
