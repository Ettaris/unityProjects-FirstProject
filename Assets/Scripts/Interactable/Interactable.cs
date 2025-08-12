using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

    private InputAction _interactAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    public virtual void Interact()
    {
        if (CanInteract())
        {
            Debug.Log($"{gameObject.name} was interacted with.");
            // Override this in subclasses for custom behavior
        }
    }

    public virtual bool CanInteract()
    {
        return true;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("InteractableColliderPlayer"))
    //    {
    //        //TODO: make string with interact prompt
    //        if (_interactAction.WasPressedThisFrame())
    //        {
    //            //TODO: realise it
    //            Interact();
    //        }
    //    }
    //}
}
