using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
            _lastAttackTime = Time.time;
            Debug.Log("Enemy attacks player");
            enemy._animator.SetTrigger("Attack");
        }

    }

    public void Exit(EnemyController enemy)
    {
    }

    //public void EnemyAttack()
    //{
    //    LayerMask playerLayer = LayerMask.GetMask("Player");

    //    Collider2D hitPlayer =
    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<EnemyController>()?.TakeDamage(damage);
    //    }

    //    Debug.Log("Sword damage applied.");
    //}

}
