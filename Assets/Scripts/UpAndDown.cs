using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the up and down movement
    public Vector3 startPosition; // Starting position of the object
    public float height = 1.0f; // Height of the up and down movement
    private float time; // Time variable to control the movement

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position based on a sine wave
        time += Time.deltaTime * speed;
        float newY = startPosition.y + Mathf.Sin(time) * height;
        // Update the object's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);


    }

    void OnDrawGizmos()
    {
        // Draw a line to visualize the movement path
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + height, startPosition.z));
        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y - height, startPosition.z));
    }
}
