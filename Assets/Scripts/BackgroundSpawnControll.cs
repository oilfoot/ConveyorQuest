using UnityEngine;

public class BackgroundSpawnControll : MonoBehaviour
{
    public GameObject Background;
    public float moveSpeed = 1.0f;
    public float respawnXPosition = 32f;

    void Start()
    {
        // Spawn prefab at specified positions
        SpawnPrefab(new Vector3(-16f, 0f, 0f));
        SpawnPrefab(new Vector3(0f, 0f, 0f));
        SpawnPrefab(new Vector3(16f, 0f, 0f));
        SpawnPrefab(new Vector3(32f, 0f, 0f));
    }

    void FixedUpdate()
    {
        // Move all spawned prefabs to the left
        MovePrefabsLeft();
    }

    void SpawnPrefab(Vector3 position)
    {
        Instantiate(Background, position, Quaternion.identity).tag = "SpawnedBackground";
    }

    void MovePrefabsLeft()
    {
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("SpawnedBackground");

        foreach (GameObject prefab in prefabs)
        {
            // Move based on fixed delta time to keep consistent movement
            prefab.transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);
            if (prefab.transform.position.x < -32f)
            {
                float newXPosition = 32f; // Adjust this if necessary to ensure alignment
                Destroy(prefab);
                SpawnPrefab(new Vector3(newXPosition, 0f, 0f));
            }
        }
    }

}
