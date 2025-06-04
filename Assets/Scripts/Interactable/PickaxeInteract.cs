using UnityEngine;

public class PickaxeInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<Pickaxe>(true).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}