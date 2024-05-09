using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    public GameObject spawnPrefab; // Reference to the prefab to spawn
    private ScoreManager scoreManager; // Reference to the ScoreManager script

    private void Start()
    {
        // Find the ScoreManager GameObject in the scene
        scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger collision is with an object tagged as "collider"
        if (other.CompareTag("collider"))
        {
            // Destroy the GameObject
            Destroy(gameObject);

            // Add 10 points to the score
            scoreManager.AddScore(10);
        }

        // Check if the trigger collision is with an object tagged as "player"
        if (other.CompareTag("player"))
        {
            // Spawn the prefab at the same position
            GameObject spawnedObject = Instantiate(spawnPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody of the spawned prefab
            Rigidbody2D spawnedRigidbody = spawnedObject.GetComponent<Rigidbody2D>();

            // Transfer the velocity from the box to the spawned prefab
            Rigidbody2D boxRigidbody = GetComponent<Rigidbody2D>();
            if (boxRigidbody != null && spawnedRigidbody != null)
            {
                spawnedRigidbody.velocity = boxRigidbody.velocity;
            }

            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
