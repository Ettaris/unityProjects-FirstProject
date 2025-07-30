using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 1;
    public GameObject endPanel;

    private int _currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = health;
    }


    public void TakeDamage(int damagePoints)
    {
        _currentHealth -= damagePoints;
        if (_currentHealth <= 0)
        {
            Debug.Log("Player is dying");
            endPanel.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
