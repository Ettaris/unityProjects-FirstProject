using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Press E to interact";

    public virtual void Interact()
    {
        Debug.Log($"{gameObject.name} was interacted with.");
        // Override this in subclasses for custom behavior
    }

    
}
