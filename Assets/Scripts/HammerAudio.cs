using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAudio : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    public AudioClip HammerHit;
    public AudioClip HammerPoint;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>(); // Find and assign ScoreManager script
    }

    // Method to play the hammer hit sound effect
    public void PlayHammerHitSound()
    {
        if (HammerHit != null)
        {
            SFXSource.PlayOneShot(HammerHit);
            SFXSource.PlayOneShot(HammerPoint);
            if (scoreManager != null)
            {
                scoreManager.AddScore(10); // Increase score by 10
            }
            else
            {
                Debug.LogWarning("ScoreManager not found!");
            }
        }
        else
        {
            Debug.LogWarning("Hammer hit sound clip is not assigned!");
        }
    }
}
