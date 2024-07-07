using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for SceneManager

public class UIHandler : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the UI GameObject to be activated when game is over
    public GameHandler gameHandler; // Reference to the GameHandler script
    public GameObject requiredGameObject; // Reference to the GameObject that needs to be active

    void Update()
    {
        // Check if the game is not active in the GameHandler
        if (!gameHandler.gameActive)
        {
            // Activate the game over UI
            gameOverUI.SetActive(true);
        }
        else
        {
            // Ensure the game over UI is deactivated if game is still active
            gameOverUI.SetActive(false);
        }

        // Check if the Square button on the PS4 controller is pressed
        
    }

    // Method to handle button click to restart the game
    public void ButtonRestart()
    {
        // Check if the requiredGameObject is active
        if (!requiredGameObject.activeInHierarchy)
        {
            Debug.LogWarning("Required GameObject is not active. Restart aborted.");
            return;
        }

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
