using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform[] lines; // Array to hold the positions of the lines
    public AudioClip swooshSound; // Sound to play when moving up or down
    public float moveUpDelay = 1.5f;
    private int currentLineIndex = 1; // Index of the current line the player is on
    [SerializeField] public int currentLineNumber; // Public variable to determine the current line number
    private AudioSource audioSource; // Reference to the AudioSource component

    private bool isMoving = false; // Flag to prevent rapid movement
    private bool inputReleased = true; // Flag to check if the input has been released

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("audio").GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        // Check for input to move the player up or down
        float verticalInput = Input.GetAxis("Vertical");

        if (!isMoving && inputReleased)
        {
            if (verticalInput > 0.5f)
            {
                StartCoroutine(MoveUp());
            }
            else if (verticalInput < -0.5f)
            {
                StartCoroutine(MoveDown());
            }
        }

        // Reset inputReleased flag when the joystick is neutral
        if (Mathf.Abs(verticalInput) < 0.1f)
        {
            inputReleased = true;
        }
    }

    IEnumerator MoveUp()
    {
        if (currentLineIndex > 0 && lines[currentLineIndex - 1].gameObject.activeSelf)
        {
            isMoving = true; // Set flag to prevent rapid movement
            inputReleased = false; // Mark input as consumed
            StartCoroutine(UpdateCurrentLineNumberCoroutine()); // Start the coroutine
            currentLineIndex--; // Move the player up immediately
            transform.position = lines[currentLineIndex].position;
            PlaySwooshSound();
            yield return new WaitForSeconds(moveUpDelay); // Wait for the delay
            isMoving = false; // Reset flag
        }
    }

    IEnumerator MoveDown()
    {
        if (currentLineIndex < lines.Length - 1 && lines[currentLineIndex + 1].gameObject.activeSelf)
        {
            isMoving = true; // Set flag to prevent rapid movement
            inputReleased = false; // Mark input as consumed
            currentLineIndex++;
            transform.position = lines[currentLineIndex].position;
            UpdateCurrentLineNumber();
            PlaySwooshSound();
            yield return new WaitForSeconds(moveUpDelay); // Wait for the delay
            isMoving = false; // Reset flag
        }
    }

    void UpdateCurrentLineNumber()
    {
        currentLineNumber = currentLineIndex + 1;
    }

    IEnumerator UpdateCurrentLineNumberCoroutine()
    {
        yield return new WaitForSeconds(moveUpDelay);
        UpdateCurrentLineNumber();
    }

    void PlaySwooshSound()
    {
        if (audioSource != null && swooshSound != null)
        {
            audioSource.PlayOneShot(swooshSound);
        }
    }
}
