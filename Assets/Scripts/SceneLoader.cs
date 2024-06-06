using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Method to load the "GameScene"
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
