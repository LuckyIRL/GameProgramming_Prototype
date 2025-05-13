using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private WaypointManager waypointManager;
    private EnemyStateMachine stateMachine;
    private AudioManager audioManager;

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
    private RobotCloak robotCloak;


    public AudioClip danceSound;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        waypointManager = Object.FindFirstObjectByType<WaypointManager>();
        stateMachine = GetComponent<EnemyStateMachine>();
        audioManager = Object.FindFirstObjectByType<AudioManager>();
        robotCloak = player.GetComponent<RobotCloak>();

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

        bool canSeePlayer = false;

        if (robotCloak == null || !robotCloak.IsCloaked)
        {
            canSeePlayer = (distanceToPlayer < detectionRange && angle < fieldOfView / 2f &&
                            !Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer));
        }

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

        float effectiveThreshold = soundDetectionThreshold;

        // If the robot is cloaked, increase the threshold (harder to detect)
        if (robotCloak != null && robotCloak.IsCloaked)
        {
            effectiveThreshold *= 1.5f; // You can tweak this multiplier
        }

        bool canHearPlayer = (distanceToPlayer < hearingRange && SoundManager.Instance.GetNoiseLevel() >= effectiveThreshold);

        if (canHearPlayer)
        {
            if (stateMachine.currentState != EnemyState.Searching)
            {
                lastKnownPosition = SoundManager.Instance.GetNoisePosition(); // Use noise source
                stateMachine.ChangeState(EnemyState.Searching);
            }
        }

    }

    private void HandleStates()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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

                // Check if player is in attack range -> Start Dancing!
                if (distanceToPlayer < attackRange)
                {
                    stateMachine.ChangeState(EnemyState.Dancing);
                    return;
                }

                // Slow down if close to last known position
                if (agent.remainingDistance < 2f)
                {
                    agent.speed = 1f;
                }
                else
                {
                    agent.speed = 3.5f;
                }

                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    stateMachine.ChangeState(EnemyState.Searching);
                }
                break;

            case EnemyState.Searching:
                agent.isStopped = false;
                WanderRandomly();

                // Check if player is in attack range -> Start Dancing!
                if (distanceToPlayer < attackRange)
                {
                    stateMachine.ChangeState(EnemyState.Dancing);
                    return;
                }
                break;

            case EnemyState.Dancing:
                agent.isStopped = true;
                Dance();
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
            //Debug.Log($" Moving to waypoint {currentWaypoint}: {nextWaypoint.position}");
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
        //Debug.Log("The AI is dancing!");

        // Play dance audio
        //DanceAudio();

        animator.SetTrigger("Dance");

        // Stay in Dance state for a few seconds, then resume patrol
        Invoke(nameof(ResumePatrolling), 13f);
    }



    private void ResumePatrolling()
    {
        Debug.Log(" AI finished dancing, returning to patrol.");
        stateMachine.ChangeState(EnemyState.Patrolling);
    }

    private void UpdateAnimations()
    {
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        animator.SetBool("IsSearching", stateMachine.currentState == EnemyState.Searching);
        animator.SetBool("IsDancing", stateMachine.currentState == EnemyState.Dancing);
    }

    public void PlayDanceAudio()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayAudioClip(0);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(lastKnownPosition, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, soundDetectionThreshold);

        // Draw Vision Cone
        Gizmos.color = Color.green;
        Vector3 fovLine1 = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * detectionRange;
        Vector3 fovLine2 = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * detectionRange;

        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}
