using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for SceneManager

public class UIHandler : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the UI GameObject to be activated when game is over
    public GameHandler gameHandler; // Reference to the GameHandler script

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
    }

    // Method to handle button click to restart the game
    public void ButtonRestart()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
