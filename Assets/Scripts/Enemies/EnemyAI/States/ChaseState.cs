using UnityEngine;

public class ChaseState : IEnemyState
{

    private float _distance = 0f;
    public void Enter(EnemyController enemy)
    {
    }

    public void Update(EnemyController enemy)
    {
         _distance = enemy.DistanceToPlayer();
        if (enemy.IsMatchstickDetected() || enemy.IsInStaticLightZone())
        {
            enemy.ChangeState(new AvoidingState());
        }
        if (_distance <= enemy.attackRange)
        {
            enemy.ChangeState(new AttackState());
        }
        else if (_distance > enemy.chaseRange)
        {
            enemy.ChangeState(new ReturnState());
        }
        else
        {
            Vector2 direction = (enemy.playerTransform.position - enemy.transform.position).normalized;
            enemy.SetCurrentMoveDirection(direction);
            enemy.transform.position += (Vector3)(direction * enemy.speed * Time.deltaTime);
            enemy._animator.SetFloat("Speed", 1);
        }
    }

    public void Exit(EnemyController enemy)
    {
    }


}
