using UnityEngine;

public class LightBlocker : MonoBehaviour
{
    private bool inLightZone = false;
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LightZone"))
        {
            inLightZone = true;
            enemyController.ChangeState(new AvoidingState());
            //TODO: make enemy go away.
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LightZone"))
        {
            inLightZone = false;
        }
    }

    public bool IsInLightZone() => inLightZone;
}

