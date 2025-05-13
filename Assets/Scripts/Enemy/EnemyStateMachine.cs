using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState currentState = EnemyState.Idle;
    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;  // Prevent duplicate state transitions

        //Debug.Log($"[EnemyStateMachine] State changed from {currentState} to {newState}");
        currentState = newState;
        enemyAI.OnStateChanged(newState);
    }
}
