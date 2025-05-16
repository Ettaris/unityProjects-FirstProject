using UnityEngine;

public interface IEnemyState
{
    void Enter(EnemyController enemy);
    void Update(EnemyController enemy);
    void Exit(EnemyController enemy);
}
