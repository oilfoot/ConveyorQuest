using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of objects to spawn

    // Method to spawn an object with a custom spawn speed
    public void SpawnObject(float spawnSpeed)
    {
        // Check if objects to spawn are defined
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogError("No objects to spawn assigned to the SpawnPoint!");
            return;
        }

        // Select a random object from the array
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Spawn the object at the spawn position (the position of the GameObject this script is attached to)
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        // Get the Rigidbody2D component of the spawned object
        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

        // Check if Rigidbody2D exists
        if (rb != null)
        {
            // Set the velocity of the object using the custom spawn speed
            rb.velocity = Vector2.left * spawnSpeed;
        }
        else
        {
            Debug.LogError("Spawned object doesn't have a Rigidbody2D component!");
        }
    }
}
