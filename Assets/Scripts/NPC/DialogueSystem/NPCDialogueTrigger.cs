using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogueTrigger : MonoBehaviour
{
    public NPCDialogueData dialogueData;
    public DialogueManager dialogueManager;
    //TODO: think about it -- private InputAction _interactAction;

    void Start()
    {
        //_interactAction = InputSystem.actions.FindAction("Interact");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(dialogueData);
        }
    }
}
