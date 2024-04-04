using UnityEngine;

public class SpawnParameterOverride : MonoBehaviour
{
    public string levelConfigPath = "level_config"; // Path to the level configuration JSON file (without extension)
    public int selectedLevelIndex = 0; // Index of the level to be picked from the JSON file

    // Reference to the SpawnController script
    public SpawnController spawnController;

    void Start()
    {
        // Load level configurations from JSON
        LoadLevelConfigurations(selectedLevelIndex);
    }

    // Method to change the level and update configurations accordingly
    public void ChangeLevel(int newLevelIndex)
    {
        selectedLevelIndex = newLevelIndex;
        LoadLevelConfigurations(selectedLevelIndex);
    }

    // New method to get the number of levels from the JSON file
    public int GetNumberOfLevels()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(levelConfigPath);
        if (jsonFile != null)
        {
            LevelConfigWrapper configWrapper = JsonUtility.FromJson<LevelConfigWrapper>(jsonFile.text);
            return configWrapper.levels.Length;
        }
        Debug.LogError("Failed to load level configuration JSON file at path: " + levelConfigPath);
        return 0; // Return 0 if the JSON file is not found or has no levels
    }

    void LoadLevelConfigurations(int levelIndex)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(levelConfigPath);

        if (jsonFile == null)
        {
            Debug.LogError("Level configuration JSON file not found at path: " + levelConfigPath);
            return;
        }

        LevelConfigWrapper configWrapper = JsonUtility.FromJson<LevelConfigWrapper>(jsonFile.text);

        if (levelIndex >= 0 && levelIndex < configWrapper.levels.Length)
        {
            ApplyLevelConfig(configWrapper.levels[levelIndex]);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }

    void ApplyLevelConfig(LevelConfig levelConfig)
    {
        // Set spawn controller parameters from the level configuration
        spawnController.baseSpawnInterval = levelConfig.newBaseSpawnInterval;
        spawnController.baseSpawnSpeeds = levelConfig.newBaseSpawnSpeeds;
        spawnController.simultaneousSpawnChance = levelConfig.newSimultaneousSpawnChance;

        // Set spawn point isActive values
        for (int i = 0; i < Mathf.Min(levelConfig.newIsActive.Length, spawnController.spawnPoints.Length); i++)
        {
            spawnController.spawnPoints[i].isActive = levelConfig.newIsActive[i];
        }
    }
}

[System.Serializable]
public class LevelConfig
{
    public int level;
    public float newBaseSpawnInterval;
    public float[] newBaseSpawnSpeeds;
    public bool[] newIsActive;
    public float newSimultaneousSpawnChance;
}

[System.Serializable]
public class LevelConfigWrapper
{
    public LevelConfig[] levels;
}
