using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public WorldDialogueUI ui;
    private DialogueNode currentNode;

    public void StartDialogue(NPCDialogueData data)
    {
        currentNode = data.startingNode;
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
        if (line != null)
            Debug.Log(line.text);
            ui.ShowLine(line.text);

        var responses = currentNode.GetValidResponses();
        if (responses.Count > 0)
            ui.ShowResponses(responses, OnResponseSelected);
    }

    void OnResponseSelected(DialogueNode nextNode)
    {
        currentNode = nextNode;
        ShowCurrentNode();
    }
}