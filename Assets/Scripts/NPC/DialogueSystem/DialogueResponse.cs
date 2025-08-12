using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    [TextArea]
    public string responseText;
    public ScriptableCondition condition;
    public DialogueNode nextNode;

    public bool IsAvailable()
    {
        return (condition == null || condition.Evaluate());
    }

}
