using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionTrigger : MonoBehaviour
{
    private Interactable _currentInteractable;
    private bool _isInteract;

    void Update()
    {
        if (_currentInteractable != null && _isInteract)
        {
            _currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            _currentInteractable = interactable;
            Debug.Log(interactable.interactionPrompt);
            // Optionally show UI prompt here
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Interactable>() == _currentInteractable)
        {
            _currentInteractable = null;
            // Hide UI prompt here
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        _isInteract = context.ReadValue<bool>();
    }
}