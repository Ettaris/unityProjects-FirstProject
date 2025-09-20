using UnityEngine;

public class ReturnState : IEnemyState
{
    Vector2 _direction = Vector2.zero;
    Vector2 _endPoint;
    private float _returnTimer;

    public void Enter(EnemyController enemy) {
        _returnTimer = 0;
        _endPoint = (GetRandomReturnPoint(enemy.GetStartPosition(), 2f));
        Debug.Log(_direction);
    }

    public void Update(EnemyController enemy)
    {
        _returnTimer += Time.deltaTime;
        _direction = _endPoint - (Vector2)enemy.transform.position;

        if (_direction.magnitude < 0.2f)
        {
            enemy.ChangeState(new IdleState());
            return;
        }

        _direction.Normalize();
        enemy.transform.position += (Vector3)(_direction * enemy.speed * Time.deltaTime);
        if (enemy.IsMatchstickDetected())
        {
            enemy.ChangeState(new AvoidingState());
        }
        else if (enemy.DistanceToPlayer() <= enemy.chaseRange && _returnTimer > 3f)
        {
            enemy.ChangeState(new ChaseState());
        }


        enemy._animator.SetFloat("Speed", 1);
    }

    public void Exit(EnemyController enemy) { }


    Vector2 GetRandomReturnPoint(Vector2 startPoint, float radius)
    {
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        return startPoint + randomOffset;
    }

}