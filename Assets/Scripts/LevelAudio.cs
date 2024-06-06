using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    public AudioClip LevelSound;

    // Method to play the level sound effect
    public void PlayLevelSound()
    {
        if (LevelSound != null)
        {
            SFXSource.PlayOneShot(LevelSound);
        }
        else
        {
            Debug.LogWarning("Level sound clip is not assigned!");
        }
    }
}
