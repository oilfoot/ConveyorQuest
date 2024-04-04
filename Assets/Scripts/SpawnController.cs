using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnPointData
    {
        public SpawnPoint spawnPoint;
        public bool isActive;
    }

    public SpawnPointData[] spawnPoints; // Array of spawn points with their active status
    public float baseSpawnInterval = 1f; // Base spawn interval
    public float[] baseSpawnSpeeds; // Array of base spawn speeds for each spawn point
    public float simultaneousSpawnChance = 0.2f; // Chance of simultaneous spawn
    private float nextSpawnTime; // Time for the next spawn

    // Public variables for affected values
    public float affectedSpawnInterval;
    public float[] affectedSpawnSpeeds;

    // Public variables for final values
    public float finalSpawnInterval;
    public float[] finalSpawnSpeeds;

    // Boolean to stop all spawning
    public bool StopAllSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the next spawn time
        nextSpawnTime = Time.time + baseSpawnInterval;

        // Initialize affected spawn speeds array
        affectedSpawnSpeeds = new float[baseSpawnSpeeds.Length];
        // Initialize final spawn speeds array
        finalSpawnSpeeds = new float[baseSpawnSpeeds.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all spawning should be stopped
        if (StopAllSpawning)
            return;

        // Check if it's time to spawn a new object
        if (Time.time >= nextSpawnTime)
        {
            // Determine if simultaneous spawn should occur
            bool shouldSpawnSimultaneously = Random.value < simultaneousSpawnChance;

            // Choose a random spawn point
            SpawnPointData activeSpawnPointData = GetRandomActiveSpawnPoint();

            // Instruct the spawn point to spawn an object
            if (activeSpawnPointData.spawnPoint != null)
            {
                // Get the index of the spawn point in the array
                int spawnIndex = System.Array.IndexOf(spawnPoints, activeSpawnPointData);

                // Calculate final spawn speed by combining baseSpawnSpeeds and affectedSpawnSpeeds
                finalSpawnSpeeds[spawnIndex] = baseSpawnSpeeds[spawnIndex] + affectedSpawnSpeeds[spawnIndex];

                // Calculate final spawn interval
                finalSpawnInterval = baseSpawnInterval - affectedSpawnInterval;

                // Spawn the object using the calculated final spawn speed
                activeSpawnPointData.spawnPoint.SpawnObject(finalSpawnSpeeds[spawnIndex]);

                // If simultaneous spawn is enabled, spawn another object on a different spawn point
                if (shouldSpawnSimultaneously)
                {
                    SpawnPointData otherActiveSpawnPointData = GetDifferentActiveSpawnPoint(activeSpawnPointData);
                    if (otherActiveSpawnPointData.spawnPoint != null)
                    {
                        // Get the index of the other spawn point in the array
                        int otherSpawnIndex = System.Array.IndexOf(spawnPoints, otherActiveSpawnPointData);

                        // Calculate final spawn speed for the other spawn point
                        finalSpawnSpeeds[otherSpawnIndex] = baseSpawnSpeeds[otherSpawnIndex] + affectedSpawnSpeeds[otherSpawnIndex];

                        // Spawn the object using the calculated final spawn speed for the other spawn point
                        otherActiveSpawnPointData.spawnPoint.SpawnObject(finalSpawnSpeeds[otherSpawnIndex]);
                    }
                }
            }

            // Update next spawn time with final spawn interval
            nextSpawnTime = Time.time + finalSpawnInterval;
        }
    }


    // Method to get a random active spawn point from the array
    private SpawnPointData GetRandomActiveSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned to the SpawnController!");
            return new SpawnPointData();
        }

        // Filter active spawn points
        SpawnPointData[] activeSpawnPoints = System.Array.FindAll(spawnPoints, spawnPointData => spawnPointData.isActive);

        if (activeSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No active spawn points found!");
            return new SpawnPointData();
        }

        return activeSpawnPoints[Random.Range(0, activeSpawnPoints.Length)];
    }

    // Method to get a different active spawn point from the given one
    private SpawnPointData GetDifferentActiveSpawnPoint(SpawnPointData originalSpawnPointData)
    {
        // Filter active spawn points
        SpawnPointData[] activeSpawnPoints = System.Array.FindAll(spawnPoints, spawnPointData => spawnPointData.isActive && spawnPointData.spawnPoint != originalSpawnPointData.spawnPoint);

        if (activeSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No different active spawn points found!");
            return new SpawnPointData();
        }

        return activeSpawnPoints[Random.Range(0, activeSpawnPoints.Length)];
    }
}
