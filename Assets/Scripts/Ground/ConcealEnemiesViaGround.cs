using UnityEngine;

public class ConcealEnemiesViaGround : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy._isEnemyRevealed)
            {
                enemy.ConcealEnemy();
            }
            else
            {
                enemy.RevealEnemy();
            }
            //TODO: make it stop near the wall
        }
    }
}
