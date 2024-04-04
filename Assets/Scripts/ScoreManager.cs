using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    // Function to update the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Function to update the TextMeshPro object with the current score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
