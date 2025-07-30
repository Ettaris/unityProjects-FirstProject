using UnityEngine;

public class IdleState : IEnemyState
{
    public void Enter(EnemyController enemy)
    {
        enemy._animator.SetFloat("Speed", 0);
    }

    public void Update(EnemyController enemy)
    {
        if (enemy.IsMatchstickDetected())
        {
            enemy.ChangeState(new AvoidingState());
        }
        else if (enemy.DistanceToPlayer() < enemy.chaseRange && enemy.IsEnemyAggressive())
        {
            enemy.ChangeState(new ChaseState());
        }

    }

    public void Exit(EnemyController enemy)
    {
    }



}
