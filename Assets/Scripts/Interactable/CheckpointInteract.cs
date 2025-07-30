using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointInteract : MonoBehaviour
{
    public GameObject mapPanel;

    private InputAction _interactAction;

    private float _interactionCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        _interactionCooldown += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_interactAction.WasPressedThisFrame() && _interactionCooldown > 0.1f)
        {
            if (collision.CompareTag("Player"))
            {
                //TODO: realise it
                Debug.Log("Open checkpoint UI(Map, bestiary and player stats)");
                _interactionCooldown = 0;
                if (!mapPanel.activeSelf)
                {
                    mapPanel.SetActive(true);
                }
                else
                {
                    mapPanel.SetActive(false);
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapPanel.SetActive(false);
        }
    }

}
