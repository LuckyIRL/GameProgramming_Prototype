using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private WaypointManager waypointManager;
    private EnemyStateMachine stateMachine;

    private int currentWaypoint = 0;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float hearingRange = 15f;
    public float fieldOfView = 60f;
    public float soundDetectionThreshold = 1.0f;

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    private Vector3 lastKnownPosition;
    private bool playerInSight;
    private bool playerHeard;

    public AudioClip danceSound;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waypointManager = Object.FindFirstObjectByType<WaypointManager>();
        stateMachine = GetComponent<EnemyStateMachine>();

        if (waypointManager == null || waypointManager.GetWaypointCount() == 0)
        {
            Debug.LogError(" No waypoints found in WaypointManager!");
            return;
        }

        currentWaypoint = 0;  // Ensure we start from the first waypoint
        PatrolToNextWaypoint();
    }

    private void Update()
    {
        SensePlayer();
        HandleStates();
        UpdateAnimations();
    }

    private void SensePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        bool canSeePlayer = (distanceToPlayer < detectionRange && angle < fieldOfView / 2f &&
                             !Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer));

        if (canSeePlayer)
        {
            // Only update lastKnownPosition if it's a new detection
            if (stateMachine.currentState != EnemyState.Seeking)
            {
                lastKnownPosition = player.position;
                stateMachine.ChangeState(EnemyState.Seeking);
            }
            return;
        }

        bool canHearPlayer = (distanceToPlayer < hearingRange && SoundManager.Instance.GetNoiseLevel() >= soundDetectionThreshold);

        if (canHearPlayer)
        {
            if (stateMachine.currentState != EnemyState.Searching)
            {
                lastKnownPosition = player.position;
                stateMachine.ChangeState(EnemyState.Searching);
            }
        }
    }



    private void HandleStates()
    {
        switch (stateMachine.currentState)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);
                break;

            case EnemyState.Patrolling:
                agent.isStopped = false;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    PatrolToNextWaypoint();
                break;

            case EnemyState.Seeking:
                agent.isStopped = false;
                agent.SetDestination(lastKnownPosition);

                // Slow down if close to avoid excessive pushing
                if (agent.remainingDistance < 2f)
                {
                    agent.speed = 1f;  // Slow speed near target
                }
                else
                {
                    agent.speed = 3.5f; // Normal speed
                }

                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    stateMachine.ChangeState(EnemyState.Searching);
                }
                break;

            case EnemyState.Searching:
                agent.isStopped = false;
                WanderRandomly(); // Implement a wandering behavior
                break;

            // Check if the player is inside the attack range and dance
            case EnemyState.Dancing:
                if (Vector3.Distance(transform.position, player.position) < attackRange)
                {
                    Dance();
                }
                break;
        }
    }

    private void WanderRandomly()
    {
        if (Vector3.Distance(transform.position, lastKnownPosition) < 1f)
        {
            // Move randomly within a small radius
            Vector3 randomDirection = Random.insideUnitSphere * 5f;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }



    public void OnStateChanged(EnemyState newState)
    {
        agent.isStopped = (newState == EnemyState.Idle);  // Only stop movement in Idle state
    }

    private void PatrolToNextWaypoint()
    {
        if (waypointManager == null) return;

        Transform nextWaypoint = waypointManager.GetNextWaypoint(currentWaypoint);
        if (nextWaypoint != null)
        {
            Debug.Log($" Moving to waypoint {currentWaypoint}: {nextWaypoint.position}");
            agent.SetDestination(nextWaypoint.position);
            currentWaypoint = (currentWaypoint + 1) % waypointManager.GetWaypointCount();
        }
        else
        {
            Debug.LogError(" Next waypoint is NULL!");
        }
    }

    private void Dance()
    {
        Debug.Log("The AI is dancing!");

        agent.isStopped = true;
        animator.SetTrigger("Dance");

        // Play Dance Sound
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource && audioSource.clip)
        {
            audioSource.Play();
        }

        Invoke(nameof(ResumePatrolling), 5f);
    }


    private void ResumePatrolling()
    {
        Debug.Log(" AI finished dancing, returning to patrol.");
        agent.isStopped = false;
        stateMachine.ChangeState(EnemyState.Patrolling);
    }

    private void UpdateAnimations()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw Vision Cone
        Gizmos.color = Color.green;
        Vector3 fovLine1 = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * detectionRange;

        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}
