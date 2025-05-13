using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    private NavMeshAgent agent;

    [Header("Click Indicator")]
    public GameObject clickIndicatorPrefab;

    [Header("Movement Settings")]
    public float stoppingDistance = 0.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure agent is enabled and ready
        if (!agent.isOnNavMesh)
        {
            Debug.LogError("Robot is not on the NavMesh! Check NavMesh Baking.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            MoveToClickPoint();

            //if (agent.velocity.magnitude > 0)
            //{
            //    SoundManager.Instance.MakeNoise(1.5f);  // Running makes noise
            //}
            //else
            //{
            //    SoundManager.Instance.MakeNoise(0);  // Stop making noise
            //}
        }

        // Stop movement if close enough to the target
        if (agent.enabled && !agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            agent.ResetPath();
        }
    }

    private void MoveToClickPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                agent.SetDestination(hit.point);
                ShowClickIndicator(hit.point);
            }
            else
            {
                Debug.LogWarning("NavMeshAgent is disabled or not on a NavMesh!");
            }
        }
    }

    private void ShowClickIndicator(Vector3 position)
    {
        if (clickIndicatorPrefab != null)
        {
            GameObject indicator = Instantiate(clickIndicatorPrefab, position, Quaternion.identity);
            Destroy(indicator, 1.5f);
        }
    }
}
