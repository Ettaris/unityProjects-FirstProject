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
            enemy.transform.position += (Vector3)(direction * enemy.speed * Time.deltaTime);
            enemy._animator.SetFloat("Speed", 1);
        }
    }

    public void Exit(EnemyController enemy)
    {
    }


}
