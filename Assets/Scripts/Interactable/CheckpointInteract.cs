using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointInteract : MonoBehaviour
{

    private InputAction _interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_interactAction.WasPressedThisFrame()) {
                //TODO: realise it
                Debug.Log("Open checkpoint UI(Map, bestiary and player stats)");
            }
        }
    }

}
