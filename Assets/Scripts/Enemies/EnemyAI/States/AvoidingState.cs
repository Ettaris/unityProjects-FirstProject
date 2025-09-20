using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AvoidingState : IEnemyState
{
    private List<Vector2> _matchPosition;
    private Vector2 _avoidingDirection;

    public void Enter(EnemyController enemy)
    {
        _matchPosition = enemy.MatchstickPosition();
        enemy.RevealEnemy();
        if (enemy.IsInStaticLightZone())
        {
            Debug.Log(enemy.IsInStaticLightZone() + "if enemy in is li");
            enemy.ChangeState(new ReturnState());
        }
    }


    public void Update(EnemyController enemy)
    {

        //TODO: make enemy go back to start point instead of opposite direction.
        if (_avoidingDirection == Vector2.zero)
        {

            SetAvoidingDirection(enemy);

            enemy._animator.SetFloat("Speed", 1);
        }
        if (enemy.IsMatchstickDetected())
        {

            enemy.transform.position += (Vector3)(_avoidingDirection.normalized * enemy.speed * 1.35f * Time.deltaTime);

        }
        else if (enemy.avoidTime > 0)
        {

            enemy.transform.position += (Vector3)(_avoidingDirection.normalized * enemy.speed * 1.35f * Time.deltaTime);
            enemy.avoidTime -= Time.deltaTime;
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit(EnemyController enemy)
    {
        enemy.ResetAvoidTime();
    }

    public void SetAvoidingDirection(EnemyController enemy)
    {

        List<Vector2> vectors = new List<Vector2>();
        foreach (var position in _matchPosition)
        {
            if (position != null)
            {
                vectors.Add((Vector2)enemy.transform.position - position);
            }
        }
        if (vectors.Count == 1)
        {
            _avoidingDirection = vectors[0];
        }
        else
        {
            foreach (var vector in vectors)
            {
                _avoidingDirection.x += vector.x;
                _avoidingDirection.y += vector.y;
            }
        }
    }

}
