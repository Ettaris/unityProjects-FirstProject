using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldDialogueUI : MonoBehaviour
{
    public Canvas worldCanvas;
    public TextMeshProUGUI dialogueText;
    public List<TextMeshProUGUI> responseTexts;

    public TypewriterText typewriter;

    private int currentResponseIndex = 0;
    private List<DialogueResponse> currentResponses;
    private System.Action<DialogueNode> onResponseSelected;
    private bool _isTextWasWritten;

    public Color defaultColor = Color.white;
    public Color selectedColor = Color.yellow;

    public void ShowLineAndResponses(string text, List<DialogueResponse> responses, System.Action<DialogueNode> onResponseSelected)
    {
        dialogueText.text = "";
        typewriter.Display(text);
        StartCoroutine(ResponseTextDelay(typewriter.GetTimeForTypingText(text), responses, onResponseSelected));
    }

    public void ShowResponses(List<DialogueResponse> responses, System.Action<DialogueNode> onResponseSelected)
    {
        this.currentResponses = responses;
        this.onResponseSelected = onResponseSelected;

        if (responses != null)
        {
            for (int i = 0; i < responseTexts.Count; i++)
            {
                if (i < responses.Count)
                {
                    responseTexts[i].text = (i + 1) + ". " + responses[i].responseText;
                    responseTexts[i].color = defaultColor;
                    responseTexts[i].gameObject.SetActive(true);
                }
                else
                {
                    responseTexts[i].gameObject.SetActive(false);
                }
            }
        }
        currentResponseIndex = -1;
    }

    public void HandleInput()
    {
        if (currentResponses == null) return;

        for (int i = 0; i < currentResponses.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HighlightResponse(i);
                SelectResponse(i);
                break;
            }
        }
    }

    private void HighlightResponse(int index)
    {
        for (int i = 0; i < responseTexts.Count; i++)
        {
            responseTexts[i].color = (i == index) ? selectedColor : defaultColor;
        }
    }

    private void SelectResponse(int index)
    {
        if (index >= 0 && index < currentResponses.Count)
        {
            onResponseSelected?.Invoke(currentResponses[index].nextNode);
            currentResponses = null;
            foreach (var text in responseTexts)
                text.gameObject.SetActive(false);
        }
    }

    IEnumerator ResponseTextDelay(float delay, List<DialogueResponse> responses, System.Action<DialogueNode> onResponseSelected)
    {
        yield return new WaitForSeconds(delay + 0.5f);
        ShowResponses(responses, onResponseSelected);
        yield return new WaitForSeconds(3f);
        if (responses.Count == 0)
            EndDialogue();
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        //TODO:
    }
}