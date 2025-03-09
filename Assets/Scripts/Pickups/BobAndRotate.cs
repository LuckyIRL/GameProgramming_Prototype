using UnityEngine;

public class BobAndRotate : MonoBehaviour
{
    public float bobSpeed = 1.0f;
    public float bobHeight = 0.1f;
    public float rotateSpeed = 1.0f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + Vector3.up * bobHeight;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, (Mathf.Sin(Time.time * bobSpeed) + 1.0f) / 2.0f);
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPosition, 0.1f);
        Gizmos.DrawSphere(endPosition, 0.1f);
    }
}
