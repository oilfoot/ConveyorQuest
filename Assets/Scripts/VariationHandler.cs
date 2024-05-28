using UnityEngine;
using System.Collections;

public class VariationHandler : MonoBehaviour
{
    public SpawnController spawnController;

    [System.Serializable]
    public struct SpawnPointToggle
    {
        public bool isActive;
    }

    public SpawnPointToggle[] spawnPointToggles;

    public float interval = 15f; // Interval for checking deactivation
    public float deactivationDuration = 5f; // Duration for which a spawn point is deactivated
    public float deactivationChance = 0.25f; // Chance for deactivating a spawn point

    // Start is called before the first frame update
    void Start()
    {
        if (spawnController == null)
        {
            Debug.LogError("SpawnController is not assigned to VariationHandler!");
            return;
        }

        if (spawnController.spawnPoints.Length != spawnPointToggles.Length)
        {
            Debug.LogError("The number of spawn point toggles does not match the number of spawn points in the SpawnController!");
            return;
        }

        // Initialize the spawn point states based on the toggles
        for (int i = 0; i < spawnPointToggles.Length; i++)
        {
            spawnController.spawnPoints[i].isActive = spawnPointToggles[i].isActive;
        }

        // Start the deactivation coroutine
        StartCoroutine(DeactivateRandomSpawnPoint());
    }

    // Update is called once per frame
    void Update()
    {
        // Update the spawn point states based on the toggles
        for (int i = 0; i < spawnPointToggles.Length; i++)
        {
            spawnController.spawnPoints[i].isActive = spawnPointToggles[i].isActive;
        }
    }

    // Coroutine to deactivate a random spawn point at intervals
    private IEnumerator DeactivateRandomSpawnPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Check if StopAllSpawning is true
            if (spawnController.StopAllSpawning)
            {
                // Ensure all spawn points are active
                for (int i = 0; i < spawnController.spawnPoints.Length; i++)
                {
                    spawnController.spawnPoints[i].isActive = true;
                    spawnPointToggles[i].isActive = true;
                }
                continue; // Skip deactivation if StopAllSpawning is true
            }

            if (Random.value < deactivationChance)
            {
                int randomIndex = Random.Range(0, spawnController.spawnPoints.Length);
                if (spawnPointToggles[randomIndex].isActive)
                {
                    // Deactivate the selected spawn point
                    spawnController.spawnPoints[randomIndex].isActive = false;
                    spawnPointToggles[randomIndex].isActive = false;

                    // Wait for the deactivation duration
                    yield return new WaitForSeconds(deactivationDuration);

                    // Reactivate the spawn point if StopAllSpawning is still false
                    if (!spawnController.StopAllSpawning)
                    {
                        spawnController.spawnPoints[randomIndex].isActive = true;
                        spawnPointToggles[randomIndex].isActive = true;
                    }
                }
            }
        }
    }
}
