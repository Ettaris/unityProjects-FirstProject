using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterText : MonoBehaviour
{
    [Header("ќзвучка и показ текста")]
    public float characterDelay = 0.02f;
    public AudioClip voiceSound;
    public AudioSource voiceSource;
    private TextMeshProUGUI _textComponent;
    private Coroutine _typingCoroutine;

    void Awake() => _textComponent = GetComponentInChildren<TextMeshProUGUI>();


    public void Display(string message)
    {
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        _typingCoroutine = StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        _textComponent.text = "";
        voiceSource.clip = voiceSound;
        foreach (char c in message)
        {
            _textComponent.text += c;
            voiceSource?.Play();
            yield return new WaitForSeconds(characterDelay);
        }
    }

    public float GetTimeForTypingText(string message) => message.Length * characterDelay;
}
