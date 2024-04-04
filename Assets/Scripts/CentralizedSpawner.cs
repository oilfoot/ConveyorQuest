using UnityEngine;
using System.Collections.Generic;

public class CentralizedSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float speed = 5f; // Changed type to float
    public Transform[] spawnPoints;
    public bool[] spawnPointActive;

    [SerializeField]
    private float spawnFrequency = 1f;
    private float timeSinceLastSpawn = 0f;

    private int[] spawnPointCounters; // Array to hold counters for each spawn point
    private int lastSpawnPointIndex = -1; // Index of the last spawned point
    private int previousSpawnPointIndex = -1; // Index of the previously spawned point

    public int maxConsecutiveSpawnsPerPoint = 3; // Maximum consecutive spawns per point
    public int simultaneousSpawnCount = 1; // Number of points to spawn simultaneously
    public float chanceOfSimultaneousSpawn = 0.5f; // Chance of spawning multiple objects simultaneously

    void Start()
    {
        // Initialize counters for each spawn point
        spawnPointCounters = new int[spawnPoints.Length];
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnFrequency)
        {
            for (int i = 0; i < simultaneousSpawnCount; i++)
            {
                if (Random.value <= chanceOfSimultaneousSpawn) // Check if chance allows simultaneous spawn
                {
                    SpawnObject();
                }
                else
                {
                    SpawnSingleObject();
                }
            }
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnObject()
    {
        int randomIndex = GetRandomActiveSpawnPointIndex();
        if (randomIndex == -1)
            return;

        // If the counter for the selected spawn point is less than maxConsecutiveSpawnsPerPoint, spawn at that point
        if (spawnPointCounters[randomIndex] < maxConsecutiveSpawnsPerPoint)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.left * speed;
            }
            else
            {
                Debug.LogWarning("The spawned object does not have a Rigidbody2D component.");
            }

            // Increment the counter for the selected spawn point
            spawnPointCounters[randomIndex]++;

            // Update the last spawned point index
            lastSpawnPointIndex = randomIndex;
        }
        else
        {
            // Find another active spawn point that is different from the last and previous spawn points
            int newRandomIndex;

            // Check if there are only two active spawn points
            if (ActiveSpawnPointCount() == 2)
            {
                newRandomIndex = GetOtherActiveSpawnPointIndex(lastSpawnPointIndex);
            }
            else
            {
                do
                {
                    newRandomIndex = GetRandomActiveSpawnPointIndex();
                } while (newRandomIndex == lastSpawnPointIndex || newRandomIndex == previousSpawnPointIndex);
            }

            // Reset the counter for the new spawn point
            spawnPointCounters[newRandomIndex] = 1;

            // Spawn on the new point
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoints[newRandomIndex].position, Quaternion.identity);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.left * speed;
            }
            else
            {
                Debug.LogWarning("The spawned object does not have a Rigidbody2D component.");
            }

            // Update the last and previous spawned point indices
            previousSpawnPointIndex = lastSpawnPointIndex;
            lastSpawnPointIndex = newRandomIndex;
        }
    }

    void SpawnSingleObject()
    {
        int randomIndex = GetRandomActiveSpawnPointIndex();
        if (randomIndex == -1)
            return;

        // If the counter for the selected spawn point is less than maxConsecutiveSpawnsPerPoint, spawn at that point
        if (spawnPointCounters[randomIndex] < maxConsecutiveSpawnsPerPoint)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.left * speed;
            }
            else
            {
                Debug.LogWarning("The spawned object does not have a Rigidbody2D component.");
            }

            // Increment the counter for the selected spawn point
            spawnPointCounters[randomIndex]++;

            // Update the last spawned point index
            lastSpawnPointIndex = randomIndex;
        }
    }

    int GetRandomActiveSpawnPointIndex()
    {
        var activeSpawnPointIndices = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPointActive[i])
            {
                // Check if the current spawn point has not reached the maximum consecutive spawns
                if (i != lastSpawnPointIndex || spawnPointCounters[i] < maxConsecutiveSpawnsPerPoint)
                {
                    activeSpawnPointIndices.Add(i);
                }
            }
        }

        if (activeSpawnPointIndices.Count == 0)
        {
            return -1;
        }

        return activeSpawnPointIndices[Random.Range(0, activeSpawnPointIndices.Count)];
    }

    // Get the index of the other active spawn point when only two are active
    int GetOtherActiveSpawnPointIndex(int currentIndex)
    {
        for (int i = 0; i < spawnPointActive.Length; i++)
        {
            if (spawnPointActive[i] && i != currentIndex)
            {
                return i;
            }
        }
        return -1; // No other active spawn point found
    }

    // Count the number of active spawn points
    int ActiveSpawnPointCount()
    {
        int count = 0;
        for (int i = 0; i < spawnPointActive.Length; i++)
        {
            if (spawnPointActive[i])
            {
                count++;
            }
        }
        return count;
    }

    public void SetSpawnFrequency(float newFrequency)
    {
        spawnFrequency = newFrequency;
    }

    public void SetChanceOfSimultaneousSpawn(float newChance)
    {
        chanceOfSimultaneousSpawn = Mathf.Clamp01(newChance); // Ensure the chance value is clamped between 0 and 1
    }
}
