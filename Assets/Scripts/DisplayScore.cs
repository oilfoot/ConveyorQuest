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
    public TextMeshProUGUI timestampText; // Add this line to declare a new TMP object for timestamps

    private void Start()
    {
        LoadScores();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton2))
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
            List<string> timestamps = new List<string>(); // Add this line to declare a list for timestamps
            HashSet<string> uniqueEntries = new HashSet<string>(); // Add this line to declare a HashSet for unique entries

            foreach (string line in lines)
            {
                string[] parts = line.Trim().Split(';');
                if (parts.Length == 3) // Update the condition to check for three parts
                {
                    int score = int.Parse(parts[0]);
                    string playerName = parts[1].Trim();
                    string timestamp = parts[2].Trim(); // Add this line to parse the timestamp
                    string entry = $"{score};{playerName};{timestamp}";

                    if (uniqueEntries.Add(entry)) // Add this line to check if the entry is unique
                    {
                        playerNames.Add(playerName);
                        scores.Add(score);
                        timestamps.Add(timestamp); // Add this line to add the timestamp to the list
                    }
                }
            }

            // Load temporary score from PlayerPrefs if available
            if (PlayerPrefs.HasKey("TempScore") && PlayerPrefs.HasKey("TempPlayerName"))
            {
                int tempScore = PlayerPrefs.GetInt("TempScore");
                string tempPlayerName = PlayerPrefs.GetString("TempPlayerName");
                string tempTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Add this line to get the current timestamp
                string tempEntry = $"{tempScore};{tempPlayerName};{tempTimestamp}";

                if (uniqueEntries.Add(tempEntry)) // Add this line to check if the temp entry is unique
                {
                    playerNames.Add(tempPlayerName);
                    scores.Add(tempScore);
                    timestamps.Add(tempTimestamp); // Add this line to add the temporary timestamp to the list
                }

                // Clear the temporary score
                PlayerPrefs.DeleteKey("TempScore");
                PlayerPrefs.DeleteKey("TempPlayerName");
                PlayerPrefs.Save();
            }

            // Sorting player names, scores, and timestamps by scores in descending order
            var sortedEntries = playerNames
                .Select((name, index) => new { name, score = scores[index], timestamp = timestamps[index] })
                .OrderByDescending(x => x.score)
                .Take(12) // Add this line to take only the top 12 entries
                .ToList();

            var sortedPlayerNames = sortedEntries.Select(x => x.name).ToList();
            var sortedScores = sortedEntries.Select(x => x.score).ToList();
            var sortedTimestamps = sortedEntries.Select(x => x.timestamp).ToList();

            // Displaying sorted scores, player names, and timestamps
            playerNameText.text = string.Join("\n", sortedPlayerNames);
            scoreText.text = string.Join("\n", sortedScores);
            timestampText.text = string.Join("\n", sortedTimestamps); // Add this line to display the timestamps
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
