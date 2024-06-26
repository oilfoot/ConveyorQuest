using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        LoadScores();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadGameScene();
        }
    }

    public void LoadScores()
    {
        // Determine the file path
        string filePath;

#if UNITY_EDITOR
        filePath = Path.Combine(Application.dataPath, "Resources/Prefs/score.txt");
#else
        filePath = Path.Combine(Application.dataPath, "score.txt");
#endif

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> playerNames = new List<string>();
            List<int> scores = new List<int>();

            foreach (string line in lines)
            {
                string[] parts = line.Trim().Split(';');
                if (parts.Length == 2)
                {
                    int score = int.Parse(parts[0]);
                    string playerName = parts[1].Trim();
                    playerNames.Add(playerName);
                    scores.Add(score);
                }
            }

            // Load temporary score from PlayerPrefs if available
            if (PlayerPrefs.HasKey("TempScore") && PlayerPrefs.HasKey("TempPlayerName"))
            {
                int tempScore = PlayerPrefs.GetInt("TempScore");
                string tempPlayerName = PlayerPrefs.GetString("TempPlayerName");

                playerNames.Add(tempPlayerName);
                scores.Add(tempScore);

                // Clear the temporary score
                PlayerPrefs.DeleteKey("TempScore");
                PlayerPrefs.DeleteKey("TempPlayerName");
                PlayerPrefs.Save();
            }

            // Sorting player names and scores by scores in descending order
            var sortedScores = scores.OrderByDescending(x => x).ToList();
            var sortedPlayerNames = playerNames
                .Select((name, index) => new { name, score = scores[index] })
                .OrderByDescending(x => x.score)
                .Select(x => x.name)
                .ToList();

            // Displaying sorted scores and player names
            playerNameText.text = string.Join("\n", sortedPlayerNames);
            scoreText.text = string.Join("\n", sortedScores);
        }
        else
        {
            Debug.LogError("Failed to load score file at path: " + filePath);
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
