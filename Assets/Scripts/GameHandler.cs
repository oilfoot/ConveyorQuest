using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public bool gameActive = true; // Flag indicating if the game is active

    private void Update()
    {
        // Check if the player's current health is greater than 0
        if (playerHealth.currentHealth <= 0)
        {
            // Set gameActive to false if player's health is 0 or less
            gameActive = false;
            //Debug.Log("Game Over!"); // Optional: Log a message indicating game over
        }
    }
}
