using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The player the camera follows
    public float smoothSpeed = 0.125f;
    public Vector3 offset;          // Default offset from the player

    public float mouseSensitivity = 100f; // Sensitivity for mouse input
    public float maxPitch = 80f;          // Maximum pitch (up/down angle)
    public float minPitch = -80f;         // Minimum pitch (down angle)

    private float yaw = 0f;               // Horizontal rotation
    private float pitch = 0f;             // Vertical rotation

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the screen
    }

    void Update()
    {
        HandleMouseInput();
    }

    void LateUpdate()
    {
        OrbitCamera();
    }

    void HandleMouseInput()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update yaw and pitch
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); // Clamp pitch to prevent over-rotation
    }

    void OrbitCamera()
    {
        // Calculate new camera position and rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(target);
    }

    // Optional setters for external scripts to adjust camera properties
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }

    public void SetSmoothSpeed(float newSmoothSpeed)
    {
        smoothSpeed = newSmoothSpeed;
    }
}
