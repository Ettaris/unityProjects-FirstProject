using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogueTrigger : MonoBehaviour
{
    public NPCDialogueData dialogueData;
    public DialogueManager dialogueManager;
    public WorldDialogueUI dialogueUI;

    //TODO: think about it -- private InputAction _interactAction;

    void Start()
    {
        //_interactAction = InputSystem.actions.FindAction("Interact");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.ui = dialogueUI;
            List<TextMeshProUGUI> responseTexts = other.GetComponentsInChildren<TextMeshProUGUI>().ToList();
            dialogueManager.StartDialogue(dialogueData, responseTexts);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueManager.EndDialogue();
    }
}
