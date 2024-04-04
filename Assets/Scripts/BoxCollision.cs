using UnityEngine;

public class BoxCollision : MonoBehaviour
{
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

        if (other.CompareTag("player"))
        {
            // Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
