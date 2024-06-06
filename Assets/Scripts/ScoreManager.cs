using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText; // Text object to display the highscore
    public AudioSource audioSource; // AudioSource for playing sound effects
    public AudioClip highscoreClip; // AudioClip for the highscore sound effect
    public DisplayLevel displayLevel; // Reference to the DisplayLevel script
    private int score = 0;
    private int highscore = 0;
    private bool hasPlayedHighscoreSound = false; // Flag to track if highscore sound has been played
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerHealth>(); // Find and assign PlayerHealth script
        highscore = PlayerPrefs.GetInt("Highscore", 0); // Load the highscore
        UpdateScoreText();
        UpdateHighscoreText();
    }

    void Update()
    {
        // Check if R + H + S keys are pressed together
        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.S))
        {
            ResetHighscore();
        }
    }

    // Function to update the score
    public void AddScore(int amount)
    {
        if (playerHealth.currentHealth > 0) // Check if the player is alive before adding score
        {
            score += amount;
            UpdateScoreText();
            CheckHighscore();
        }
    }

    // Function to update the TextMeshPro object with the current score
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Function to check and update the highscore
    void CheckHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore); // Save the new highscore
            PlayerPrefs.Save(); // Ensure the highscore is saved
            UpdateHighscoreText();
            if (!hasPlayedHighscoreSound) // Play the sound only once
            {
                PlayHighscoreSound(); // Play the highscore sound effect
                hasPlayedHighscoreSound = true; // Set the flag to true after playing the sound
                displayLevel.DisplayNewHighscore(); // Trigger new highscore display
            }
        }
    }

    // Function to update the TextMeshPro object with the current highscore
    void UpdateHighscoreText()
    {
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    // Function to reset the highscore
    void ResetHighscore()
    {
        highscore = 0;
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.Save();
        UpdateHighscoreText();
        hasPlayedHighscoreSound = false; // Reset the flag when the highscore is reset
    }

    // Function to play the highscore sound effect
    void PlayHighscoreSound()
    {
        if (audioSource != null && highscoreClip != null)
        {
            audioSource.clip = highscoreClip;
            audioSource.Play();
        }
    }
}
