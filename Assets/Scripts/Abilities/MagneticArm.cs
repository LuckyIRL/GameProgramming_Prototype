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

    private Rigidbody playerRb;
    private Transform targetObject;
    //private bool isPullingObject = false;
    //private bool isPullingPlayer = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    

    private void Update()
    {
        magneticFieldEffect.SetActive(targetObject != null);

        DetectMagneticObjects();

        if (Input.GetKeyDown(attractObjectKey))
            PullObjectToPlayer();

        if (Input.GetKeyDown(pullToObjectKey))
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
                Debug.Log("Magnetic object detected: " + hit.collider.name);
            }
            else
            {
                targetObject = null;
            }
        }
        else
        {
            targetObject = null;
        }
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
        if (targetObject != null)
        {
            Vector3 forceDirection = (targetObject.position - playerRb.position).normalized;
            playerRb.AddForce(forceDirection * playerPullSpeed, ForceMode.Acceleration);

            Debug.Log("Pulling player towards object with force: " + forceDirection);
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
