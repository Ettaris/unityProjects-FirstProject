using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointInteract : MonoBehaviour
{
    //TODO: clean up this script
    public GameObject mapPanel;
    public GameObject matchesOnTable;
    public int amountOfMatchesToPickUp = 3;

    private InputAction _interactAction;
    private bool _isActivated;
    private float _interactionCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(_interactionCooldown);
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("interacted");
                PlayerInteract playerInteract = collision.GetComponent<PlayerInteract>();
                if (!playerInteract.IsInteracting())
                {

                    playerInteract.SetCurrentInteraction(this);
                    collision.GetComponent<Animator>().SetTrigger("InteractOn");
                }
                else
                {
                }
            }
        }



    }

    public void EndInteraction()
    {
        _isActivated = true;
    }

    public void Interact()
    {
        Debug.Log("Open checkpoint UI(Map, bestiary and player stats)");
        if (!mapPanel.activeSelf)
        {
            mapPanel.SetActive(true);
        }
        else
        {
            mapPanel.SetActive(false);
        }
    }

    public void PickUpMatchesFromTable()
    {
        matchesOnTable.SetActive(false);
    }

    public bool IsActivated() => _isActivated;

}
