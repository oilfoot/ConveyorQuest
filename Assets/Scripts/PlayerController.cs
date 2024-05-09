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

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("audio").GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        // Check for input to move the player up or down
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        // If the player is not already on the top line and the line above is active, move up
        if (currentLineIndex > 0 && lines[currentLineIndex - 1].gameObject.activeSelf)
        {
            StartCoroutine(UpdateCurrentLineNumberCoroutine()); // Start the coroutine
            currentLineIndex--; // Move the player up immediately
            transform.position = lines[currentLineIndex].position;
            PlaySwooshSound();
        }
    }

    void MoveDown()
    {
        // If the player is not already on the bottom line and the line below is active, move down
        if (currentLineIndex < lines.Length - 1 && lines[currentLineIndex + 1].gameObject.activeSelf)
        {
            currentLineIndex++;
            transform.position = lines[currentLineIndex].position;
            UpdateCurrentLineNumber();
            PlaySwooshSound();
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
