using UnityEngine;

public class MatchesToPickUp : MonoBehaviour
{
    [SerializeField] private int _numberOfMatchesInThisObject = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInteractionWithMatches>().PickUpMatches(_numberOfMatchesInThisObject);
            Destroy(this.gameObject);
        }
    }

}
