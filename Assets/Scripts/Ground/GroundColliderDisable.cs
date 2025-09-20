using UnityEngine;
using UnityEngine.U2D;

public class GroundColliderDisable : MonoBehaviour
{
    public SpriteShapeController groundController;
    public EdgeCollider2D groundCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Matches"))
        {
            if (groundCollider.isActiveAndEnabled)
            {
                groundCollider.enabled = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        groundCollider.enabled = true;
    }


}
