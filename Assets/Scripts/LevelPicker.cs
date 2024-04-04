using UnityEngine;
using System.Collections;

public class LevelPicker : MonoBehaviour
{
    public SpawnParameterOverride spawnParameterOverride; // Reference to the SpawnParameterOverride script
    public SpawnController spawnController; // Reference to the SpawnController script
    public float intervalTime = 10f; // Time in seconds between random level picks, customizable in the Inspector
    public float stopSpawningDuration = 5f; // Duration in seconds to stop spawning

    private int numberOfLevels; // To keep track of the total number of levels

    void Start()
    {
        // Set the initial level to 0
        spawnParameterOverride.ChangeLevel(0);

        // Determine the number of levels available from the SpawnParameterOverride script
        numberOfLevels = spawnParameterOverride.GetNumberOfLevels();

        // Start picking random levels after the initial interval time, then repeat every intervalTime seconds
        InvokeRepeating("PickRandomLevel", intervalTime, intervalTime);
    }

    void PickRandomLevel()
    {
        int randomLevelIndex = Random.Range(0, numberOfLevels);
        spawnParameterOverride.ChangeLevel(randomLevelIndex);

        // Activate StopAllSpawning in SpawnController for the specified duration
        StartCoroutine(ActivateStopAllSpawningForDuration(stopSpawningDuration));
    }

    IEnumerator ActivateStopAllSpawningForDuration(float duration)
    {
        // Set StopAllSpawning to true
        spawnController.StopAllSpawning = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Set StopAllSpawning back to false
        spawnController.StopAllSpawning = false;
    }
}
