using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

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
}
