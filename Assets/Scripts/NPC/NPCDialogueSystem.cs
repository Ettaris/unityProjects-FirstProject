using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NPCDialogueSystem : Interactable
{

    public NPCDialogueData dialogueData;
    public TMP_Text dialogueText;
    public GameObject dialogueTextPlace;

    private int _dialogueIndex;
    private bool _isTyping, _isDialogueActive;

    public override void Interact()
    {
        if (dialogueData == null)
        {
            return;
        }

        if (_isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        _isDialogueActive = true;
        _dialogueIndex = 0;

        dialogueTextPlace.SetActive(true);
        dialogueText.SetText("");


        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        _isTyping = true;
        dialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[_dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        _isTyping = false;

        if (dialogueData.autoProgessLines.Length > _dialogueIndex && dialogueData.autoProgessLines[_dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    private void NextLine()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[_dialogueIndex]);
            _isTyping = false;
        }
        else if (++_dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }

    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        _isTyping = false;
        _isDialogueActive = false;
        dialogueText.SetText("");
        dialogueTextPlace.SetActive(false);
    }

    public override bool CanInteract()
    {
        return !_isDialogueActive;
    }


}
