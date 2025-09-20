using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public WorldDialogueUI ui;
    private DialogueNode currentNode;
    private DialogueNode nextNode;

    public void StartDialogue(NPCDialogueData data, List<TextMeshProUGUI> responseTexts)
    {
        currentNode = data.startingNode;
        ui.responseTexts = responseTexts;
        ShowCurrentNode();
        Debug.Log("Dialogue is started");
    }

    void Update()
    {
        ui.HandleInput();
    }

    void ShowCurrentNode()
    {
        if (currentNode == null) return;

        var line = currentNode.GetRandomLine();
        var responses = currentNode.GetValidResponses();
        if (line != null)
            ui.ShowLineAndResponses(line.text, responses, OnResponseSelected);

    }

    void OnResponseSelected(DialogueNode nextNode)
    {
        currentNode = nextNode;
        ShowCurrentNode();
    }

    public void EndDialogue()
    {
        ui.EndDialogue();
    }
}