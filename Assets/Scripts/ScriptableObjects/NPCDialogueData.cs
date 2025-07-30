using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogueData", menuName = "NPC")]
public class NPCDialogueData : ScriptableObject
{
    public string[] dialogueLines;
    public bool[] autoProgessLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    
}
