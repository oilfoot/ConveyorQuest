using UnityEngine;
using TMPro; // Add this to use TextMeshPro

public class DisplayLevel : MonoBehaviour
{
    public SpawnController spawnController; // Reference to the SpawnController script
    public GameHandler gameHandler; // Reference to the GameHandler script
    public TextMeshProUGUI textToFade; // Reference to the TMP text component you want to fade
    public GameObject secondObjectToFade; // Reference to the second GameObject you want to fade
    public float fadeDuration = 1f; // Duration of the fade effect
    public float displayDuration = 2f; // Duration for which the text is displayed before fading out
    public float fadeInDelay = 1f; // Delay before starting the fade-in process
    public LevelAudio levelAudio; // Reference to the LevelAudio script

    private bool isFading = false; // Flag to track if fading is in progress
    private float fadeTimer = 0f; // Timer for the fade effect
    private float displayTimer = 0f; // Timer for the display duration
    private Color originalColor; // Original color of the text
    private Material secondObjectMaterial; // Material of the second GameObject
    private Color secondOriginalColor; // Original color of the second object
    private bool previousStopAllSpawning = false; // Track previous state of StopAllSpawning
    private int currentLevel = 1; // Start from Level 1
    private bool isDelayActive = false; // Flag to check if delay is active
    private float delayTimer = 0f; // Timer for the delay
    private bool newHighscore = false; // Flag to check if new highscore is set

    // Start is called before the first frame update
    void Start()
    {
        // Check if the SpawnController reference is assigned
        if (spawnController == null)
        {
            Debug.LogError("SpawnController reference not assigned in DisplayLevel script!");
            return;
        }

        // Check if the GameHandler reference is assigned
        if (gameHandler == null)
        {
            Debug.LogError("GameHandler reference not assigned in DisplayLevel script!");
            return;
        }

        // Ensure textToFade is not null
        if (textToFade == null)
        {
            Debug.LogError("Text to fade is not assigned in DisplayLevel script!");
            return;
        }

        // Check if the LevelAudio reference is assigned
        if (levelAudio == null)
        {
            Debug.LogError("LevelAudio reference not assigned in DisplayLevel script!");
            return;
        }

        // Ensure secondObjectToFade is not null
        if (secondObjectToFade == null)
        {
            Debug.LogError("Second object to fade is not assigned in DisplayLevel script!");
            return;
        }

        // Store the original color of the text and set initial alpha to 0 (fully transparent)
        originalColor = textToFade.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;
        textToFade.color = transparentColor;

        // Store the original color of the second object and set initial alpha to 0 (fully transparent)
        secondObjectMaterial = secondObjectToFade.GetComponent<Renderer>().material;
        secondOriginalColor = secondObjectMaterial.color;
        Color secondTransparentColor = secondOriginalColor;
        secondTransparentColor.a = 0f;
        secondObjectMaterial.color = secondTransparentColor;

        // Display the initial level text if the game is active
        if (gameHandler.gameActive)
        {
            textToFade.text = "Level " + currentLevel;
            StartFadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for state change in StopAllSpawning and gameActive status
        if (spawnController != null && spawnController.StopAllSpawning != previousStopAllSpawning)
        {
            previousStopAllSpawning = spawnController.StopAllSpawning;
            if (spawnController.StopAllSpawning && !isFading && gameHandler.gameActive)
            {
                StartDelay();
            }
        }

        // Handle delay before fade-in
        if (isDelayActive)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= fadeInDelay)
            {
                isDelayActive = false;
                StartFadeIn();
            }
        }

        // Perform fading effect if isFading is true
        if (isFading)
        {
            FadeTextAndObject();
        }
    }

    // Method to start the delay before fade-in
    private void StartDelay()
    {
        isDelayActive = true;
        delayTimer = 0f;
    }

    // Method to start the fade-in process
    private void StartFadeIn()
    {
        isFading = true;
        fadeTimer = 0f;
        displayTimer = 0f; // Reset display timer

        // Set the text based on the current state
        if (newHighscore)
        {
            textToFade.text = "New Highscore";
            newHighscore = false; // Reset the flag
        }
        else
        {
            textToFade.text = "Level " + currentLevel; // Set the text to the current level
            currentLevel++; // Increment the level for the next time
        }

        // Play the level sound
        levelAudio.PlayLevelSound();
    }

    // Method to perform fading effect
    private void FadeTextAndObject()
    {
        // Increment timers
        fadeTimer += Time.deltaTime;
        if (fadeTimer < fadeDuration)
        {
            // Fade in phase
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            SetTextAlpha(alpha);
            SetObjectAlpha(alpha);
        }
        else
        {
            // Display phase
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayDuration)
            {
                // Start fade out after display duration
                float fadeOutTime = fadeTimer - fadeDuration - displayDuration;
                float alpha = Mathf.Clamp01(1f - (fadeOutTime / fadeDuration));
                SetTextAlpha(alpha);
                SetObjectAlpha(alpha);

                if (fadeOutTime >= fadeDuration)
                {
                    isFading = false; // Stop fading
                    Debug.Log("Fade out complete.");
                }
            }
        }
    }

    // Helper method to set the text alpha
    private void SetTextAlpha(float alpha)
    {
        Color color = originalColor;
        color.a = alpha;
        textToFade.color = color;
    }

    // Helper method to set the object alpha
    private void SetObjectAlpha(float alpha)
    {
        Color color = secondOriginalColor;
        color.a = alpha;
        secondObjectMaterial.color = color;
    }

    // Public method to trigger new highscore display
    public void DisplayNewHighscore()
    {
        newHighscore = true;
        StartFadeIn();
    }
}
