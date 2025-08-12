using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [Header("Ответы НПС и игрока")]
    public List<DialogueLine> lines;
    public List<DialogueResponse> responses;

    public DialogueLine GetRandomLine()
    {
        var validLines = lines.FindAll(line => line.IsAvailable());
        if (validLines.Count == 0) return null;
        return validLines[Random.Range(0, validLines.Count)];
    }

    public List<DialogueResponse> GetValidResponses()
    {
        return responses.FindAll(r => r.IsAvailable());
    }
}
