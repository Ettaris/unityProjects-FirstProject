using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea]
    public string text;
    public ScriptableCondition condition; // Can be empty

    public bool IsAvailable()
    {
        //If there is a condition, it's check for it. Else just use this line.
        return condition == null || condition.Evaluate();
    }
}

