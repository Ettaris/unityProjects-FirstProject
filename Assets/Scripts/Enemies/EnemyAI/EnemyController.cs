using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldownTime = 1f;
    public float speed = 2f;
    public Transform playerTransform;

    private Vector2 _startPosition;
    private IEnemyState _currentState;

    private void Start()
    {
        _startPosition = transform.position;
        _currentState = new IdleState();
        _currentState.Enter(this);
    }

    private void Update()
    {
        _currentState.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public Vector2 GetStartPosition() => _startPosition;

    public float DistanceToPlayer() => Vector2.Distance(transform.position, playerTransform.position);
}

