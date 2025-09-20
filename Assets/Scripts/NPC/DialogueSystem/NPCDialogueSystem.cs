using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NPCDialogueSystem : Interactable
{

    public NPCDialogueData dialogueData;
    public DialogueStage stage = DialogueStage.FirstMeeting;
    public TMP_Text dialogueText;
    public GameObject dialogueTextPlace;
    public AudioSource audioSource;

    private int _dialogueIndex;
    private bool _isTyping, _isDialogueActive;


    //public class NPCDialogue : MonoBehaviour
    //{
    //    public DialogueData dialogueData;
    //    public DialogueStage stage = DialogueStage.Greeting;

    //    public void Interact()
    //    {
    //        var line = dialogueData.GetRandomLine(stage);

    //        if (line != null)
    //        {
    //            DialogueManager.Instance.ShowDialogue(line.text);

    //            // Если это первое знакомство — переход в Known
    //            if (stage == DialogueStage.Greeting)
    //                stage = DialogueStage.Known;
    //        }
    //        else
    //        {
    //            DialogueManager.Instance.ShowDialogue("...");
    //        }
    //    }
    //}

}
