using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreSaver : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro displaying the score
    public TMP_InputField nameInputField; // Reference to the InputField for entering the player's name

    // Function to read out the score as an integer
    public int GetScore()
    {
        int score = 0;
        int.TryParse(scoreText.text.Replace("Score: ", ""), out score);
        return score;
    }

    // Function to read the input from the InputField
    public string GetPlayerName()
    {
        return nameInputField.text;
    }

    // Function to save the player's name and score to a text file
    public void SaveScore()
    {
        string playerName = GetPlayerName();
        int score = GetScore();

        // Store the score and name in PlayerPrefs
        PlayerPrefs.SetInt("TempScore", score);
        PlayerPrefs.SetString("TempPlayerName", playerName);
        PlayerPrefs.Save();

        // Construct the file path
        string filePath = Path.Combine(Application.dataPath, "Resources/Prefs/score.txt");

        // Write the player's score and name to the text file
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true)) // Append mode
            {
                writer.WriteLine(score + ";" + playerName);
            }
            Debug.Log("Score saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save score: " + e.Message);
        }

        LoadStartScene(); // Load the StartScene after saving the score
    }

    // Function to load the StartScene
    private void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
