using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip hurtSound;
    public AudioClip swooshSound;
    public AudioClip explosionSound;
    public AudioClip gameOverSound;

    public void PlayHurtSound()
    {
        if (hurtSound != null)
        {
            SFXSource.PlayOneShot(hurtSound);
        }
        else
        {
            Debug.LogWarning("Hurt sound clip is not assigned!");
        }
    }

    // Method to play the swoosh sound effect
    public void PlaySwooshSound()
    {
        if (swooshSound != null)
        {
            SFXSource.PlayOneShot(swooshSound);
        }
        else
        {
            Debug.LogWarning("Swoosh sound clip is not assigned!");
        }
    }

    // Method to play the explosion sound effect
    public void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            SFXSource.PlayOneShot(explosionSound);
        }
        else
        {
            Debug.LogWarning("Explosion sound clip is not assigned!");
        }
    }

    public void PlayGameOverSound()
    {
        if (explosionSound != null)
        {
            SFXSource.PlayOneShot(gameOverSound);
        }
        else
        {
            Debug.LogWarning("Explosion sound clip is not assigned!");
        }
    }

}
