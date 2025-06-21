using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    private float _lastAttackTime;
    private float _attackCooldownTime;
    private float _distance;
    private EnemyController _enemy;

    public void Enter(EnemyController enemy)
    {
        _enemy = enemy;
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
            _lastAttackTime = Time.time;
            Debug.Log("Enemy attacks player");
            enemy._animator.SetTrigger("Attack");
        }

    }

    public void Exit(EnemyController enemy)
    {
        foreach (DamadeByCollision damageByCollisionObject in _enemy?.gameObject.GetComponentsInChildren<DamadeByCollision>(true))
        {
            damageByCollisionObject.enabled = false;
        }
    }



}
