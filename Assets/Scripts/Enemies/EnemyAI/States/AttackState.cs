using UnityEngine;

public class AttackState : IEnemyState
{
    private float _lastAttackTime;
    private float _attackCooldownTime;
    private float _distance;

    public void Enter(EnemyController enemy)
    {
        _attackCooldownTime = enemy.attackCooldownTime;
        _lastAttackTime = Time.time - _attackCooldownTime;
    }


    public void Update(EnemyController enemy)
    {
        _distance = enemy.DistanceToPlayer();
        if (_distance > enemy.attackRange)
        {
            enemy.ChangeState(new ChaseState());
        }
        else if (Time.time - _lastAttackTime >= _attackCooldownTime)
        {
            Debug.Log("Enemy attacks player");

        }

    }

    public void Exit(EnemyController enemy)
    {
    }


}
