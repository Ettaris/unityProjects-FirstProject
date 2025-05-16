using UnityEngine;

public class ReturnState : IEnemyState
{
    Vector2 _direction = Vector2.zero;

    public void Enter(EnemyController enemy) { }

    public void Update(EnemyController enemy)
    {
        _direction = (enemy.GetStartPosition() - (Vector2)enemy.transform.position);

        if (_direction.magnitude < 0.1f)
        {
            enemy.ChangeState(new IdleState());
            return;
        }

        _direction.Normalize();
        enemy.transform.position += (Vector3)(_direction * enemy.speed * Time.deltaTime);

        if (enemy.DistanceToPlayer() <= enemy.chaseRange)
        {
            enemy.ChangeState(new ChaseState());
        }
    }

    public void Exit(EnemyController enemy) { }
}