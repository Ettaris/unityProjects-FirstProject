using UnityEngine;

public class DamadeByCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" && isActiveAndEnabled)
        {
            Debug.Log("Player is attacked");
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(1);
        }
    }
}
