using UnityEngine;

public class IdleState : IEnemyState
{

    private float _randomTimer;

    public void Enter(EnemyController enemy)
    {
        enemy._animator.SetFloat("Speed", 0);
        _randomTimer = 0;
    }

    public void Update(EnemyController enemy)
    {
        _randomTimer += Time.deltaTime;
        if (enemy.IsMatchstickDetected())
        {
            enemy.ChangeState(new AvoidingState());
        }
        else if (enemy.DistanceToPlayer() < enemy.chaseRange && enemy.IsEnemyAggressive())
        {
            enemy.ChangeState(new ChaseState());
        }

        if (_randomTimer > 20f && (Vector2)(enemy.transform.position) != enemy.GetStartPosition())
        {
            enemy.ChangeState(new ReturnState());
        }
    }

    public void Exit(EnemyController enemy)
    {
    }



}
