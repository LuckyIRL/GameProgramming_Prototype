using UnityEngine;

public class MagneticArm : MonoBehaviour
{
    [Header("References")]
    public Transform magnetSpawnPoint;    // Position where the magnet effect originates
    public LayerMask magneticLayer;       // Layer for magnetic objects
    public GameObject magArm;
    public GameObject magneticFieldEffect;

    [Header("Settings")]
    public float attractionForce = 20f;   // Strength of the pulling force
    public float maxMagnetDistance = 15f; // Maximum magnetic pull distance
    public float playerPullSpeed = 10f;   // Speed to pull the player
    public KeyCode attractObjectKey = KeyCode.F;  // Pull object toward player
    public KeyCode pullToObjectKey = KeyCode.G;   // Pull player toward object

    private CharacterController characterController;
    private Transform targetObject;
    //private bool isPullingObject = false;
    //private bool isPullingPlayer = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }



    private void Update()
    {
        DetectMagneticObjects();

        bool targetExists = targetObject != null;
        magneticFieldEffect.SetActive(targetExists);

        if (targetExists)
        {
            UpdateMagneticFieldVisual();
        }

        if (Input.GetKey(attractObjectKey))
            PullObjectToPlayer();

        if (Input.GetKey(pullToObjectKey))
            PullPlayerToObject();

    }


    private void DetectMagneticObjects()
    {
        RaycastHit hit;
        Debug.DrawRay(magnetSpawnPoint.position, magnetSpawnPoint.forward * maxMagnetDistance, Color.blue);

        if (Physics.Raycast(magnetSpawnPoint.position, magnetSpawnPoint.forward, out hit, maxMagnetDistance, magneticLayer))
        {
            if (hit.collider.CompareTag("MagneticObject"))
            {
                targetObject = hit.collider.transform;
                magneticFieldEffect.SetActive(true);
                Debug.Log("Magnetic object detected: " + hit.collider.name);
                return; // early exit
            }
        }

        // No hit or not a magnetic object
        targetObject = null;
        magneticFieldEffect.SetActive(false);
    }


    private void UpdateMagneticFieldVisual()
    {
        Vector3 start = magnetSpawnPoint.position;
        Vector3 end = targetObject.position;
        Vector3 direction = end - start;

        // Position halfway between magnet and object
        magneticFieldEffect.transform.position = start + direction / 2f;

        // Rotate the field to face the target
        magneticFieldEffect.transform.rotation = Quaternion.LookRotation(direction);

        // Scale length-wise based on distance
        float distance = direction.magnitude;
        magneticFieldEffect.transform.localScale = new Vector3(
            0.1f,  // Width (adjust as needed)
            0.1f,  // Height
            distance  // Length toward target
        );
    }


    private void PullObjectToPlayer()
    {
        if (targetObject != null)
        {
            // Check if the object has a Rigidbody
            Rigidbody objectRb = targetObject.GetComponent<Rigidbody>();

            if (objectRb == null)
            {
                Debug.LogError("Magnetic object does not have a Rigidbody! Add one.");
                return;
            }

            Vector3 forceDirection = (magnetSpawnPoint.position - targetObject.position).normalized;
            objectRb.AddForce(forceDirection * attractionForce, ForceMode.Force);
            Debug.Log("Pulling object towards player.");
        }
    }


    private void PullPlayerToObject()
    {
        if (targetObject != null && characterController != null)
        {
            Vector3 direction = (targetObject.position - transform.position).normalized;
            characterController.Move(direction * playerPullSpeed * Time.deltaTime);
            Debug.Log("Pulling player toward object (CharacterController)");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagArmPickup"))
        {
            ActivateMagArm();
            Destroy(other.gameObject);
        }
    }

    private void ActivateMagArm()
    {
        magArm.SetActive(true);
        Debug.Log("Magnetic Arm Activated!");
    }
}
