using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float sensitivity = 3.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        transform.position = target.position + Quaternion.Euler(pitch, yaw, 0) * offset;
        transform.LookAt(target.position);
    }
}
