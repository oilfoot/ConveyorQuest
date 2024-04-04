using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip hurtSound;
    public AudioClip swooshSound;
    public AudioClip explosionSound;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Method to play the hurt sound effect
    public void PlayHurtSound()
    {
        if (hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
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
            audioSource.PlayOneShot(swooshSound);
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
            audioSource.PlayOneShot(explosionSound);
        }
        else
        {
            Debug.LogWarning("Explosion sound clip is not assigned!");
        }
    }
}
