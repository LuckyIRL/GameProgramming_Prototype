using UnityEngine;

public class RobotPhysics : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 5.0f;
        rb.linearDamping = 0.5f; // Updated from rb.drag
        rb.angularDamping = 0.5f; // Updated from rb.angularDrag
        rb.useGravity = true;

        // Add a Capsule collider for collision detection
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
        collider.height = 1.0f;
    }

    void FixedUpdate()
    {
        // Move the robot forward
        rb.AddForce(transform.forward * 10f);
    }
}
