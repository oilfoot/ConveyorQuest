using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        UpdateScoreText();
        playerHealth = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerHealth>(); // Find and assign PlayerHealth script
    }

    // Function to update the score
    public void AddScore(int amount)
    {
        if (playerHealth.currentHealth > 0) // Check if the player is alive before adding score
        {
            score += amount;
            UpdateScoreText();
        }
    }

    // Function to update the TextMeshPro object with the current score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
