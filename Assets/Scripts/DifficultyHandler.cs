using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    public SpawnController spawnController; // Reference to the SpawnController
    public float maxSpawnSpeed = 10f; // Maximum spawn speed
    public float maxSpawnIntervalValue = 0.5f; // Minimum spawn interval value, adjusted for clarity
    public float intervalIncreaseRate = 0.1f; // Rate at which spawn interval increases over time
    public float speedIncreaseRate = 0.1f; // Rate at which spawn speed increases over time
    public float spawnIntervalDifficultyCheckInterval = 20f; // Interval at which spawn interval difficulty is checked and adjusted
    public float spawnSpeedDifficultyCheckInterval = 10f; // Interval at which spawn speed difficulty is checked and adjusted
    public float intervalIncreaseValue = 0.5f; // Value by which spawn interval difficulty check interval increases
    public float speedIncreaseValue = 0.5f; // Value by which spawn speed difficulty check interval increases

    private float lastSpawnIntervalCheckTime; // Last time spawn interval difficulty was checked
    private float lastSpawnSpeedCheckTime; // Last time spawn speed difficulty was checked

    void Start()
    {
        lastSpawnIntervalCheckTime = Time.time;
        lastSpawnSpeedCheckTime = Time.time;
    }

    void Update()
    {
        // Check if it's time to adjust spawn interval difficulty
        if (Time.time - lastSpawnIntervalCheckTime >= spawnIntervalDifficultyCheckInterval)
        {
            AdjustSpawnIntervalDifficulty();
            lastSpawnIntervalCheckTime = Time.time;
            spawnIntervalDifficultyCheckInterval += intervalIncreaseValue; // Increase the spawn interval difficulty check interval
        }

        // Check if it's time to adjust spawn speed difficulty
        if (Time.time - lastSpawnSpeedCheckTime >= spawnSpeedDifficultyCheckInterval)
        {
            AdjustSpawnSpeedDifficulty();
            lastSpawnSpeedCheckTime = Time.time;
            spawnSpeedDifficultyCheckInterval += speedIncreaseValue; // Increase the spawn speed difficulty check interval
        }
    }

    void AdjustSpawnIntervalDifficulty()
    {
        // Increase spawn interval to increase difficulty
        spawnController.affectedSpawnInterval = Mathf.Min(maxSpawnIntervalValue, spawnController.affectedSpawnInterval + intervalIncreaseRate);
    }

    void AdjustSpawnSpeedDifficulty()
    {
        // Increase spawn speed
        for (int i = 0; i < spawnController.affectedSpawnSpeeds.Length; i++)
        {
            spawnController.affectedSpawnSpeeds[i] = Mathf.Min(maxSpawnSpeed, spawnController.affectedSpawnSpeeds[i] + speedIncreaseRate);
        }
    }
}
