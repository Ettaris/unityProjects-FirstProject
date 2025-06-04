using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public string checkpointID = "CP_01";
    public bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive) return;

        if (collision.CompareTag("Player"))
        {
            ActivateCheckpoint();
        }
    }

    void ActivateCheckpoint()
    {
        isActive = true;
        Debug.Log("Checkpoint activated: " + checkpointID);

        // Simulate saving game state
        //TODO: Make save/load system
        SaveGameState();

        // Optional: Visual feedback
        // e.g., change sprite, play animation or sound
    }

    void SaveGameState()
    {
        // Save position to PlayerPrefs (or use your own system)
        //TODO: Remake it
        PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
        PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
        PlayerPrefs.SetString("LastCheckpoint", checkpointID);
        PlayerPrefs.Save();

        Debug.Log("Game saved at checkpoint: " + checkpointID);
    }
}

