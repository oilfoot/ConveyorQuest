using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    public GameObject spawnPrefab; // Reference to the prefab to spawn
    public ParticleSystem[] collisionParticles; // Array of Particle Systems to spawn on collision with player
    public Transform[] particleSpawnPoints; // Empty GameObjects to define where particles spawn from
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
            // Get the order in layer of this GameObject
            int orderInLayer = GetComponent<SpriteRenderer>().sortingOrder;

            // Calculate new order in layer
            int newOrderInLayer = orderInLayer + 2;

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

            // Spawn collision particles
            for (int i = 0; i < collisionParticles.Length; i++)
            {
                if (i < particleSpawnPoints.Length && particleSpawnPoints[i] != null)
                {
                    ParticleSystem particle = Instantiate(collisionParticles[i], particleSpawnPoints[i].position, Quaternion.identity);
                    particle.GetComponent<Renderer>().sortingOrder = newOrderInLayer;
                }
                else
                {
                    // If there's no corresponding spawn point, spawn at box position
                    ParticleSystem particle = Instantiate(collisionParticles[i], transform.position, Quaternion.identity);
                    particle.GetComponent<Renderer>().sortingOrder = newOrderInLayer;
                }
            }

            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
