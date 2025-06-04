using UnityEngine;

public class IdleState : IEnemyState
{
    public void Enter(EnemyController enemy)
    {
    }

    public void Update(EnemyController enemy)
    {
        if (enemy.DistanceToPlayer() < enemy.chaseRange)
        {
            enemy.ChangeState(new ChaseState());
        }
        enemy._animator.SetFloat("Speed", 0);

    }

    public void Exit(EnemyController enemy)
    {
    }



}
