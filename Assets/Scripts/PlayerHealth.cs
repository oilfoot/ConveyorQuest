using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public CMShake cameraShake; // Reference to the CameraShake script
    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip hurtSound; // Sound to play when player is hurt
    public AudioClip explosionSound; // Sound to play when player dies

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GameObject.FindGameObjectWithTag("audio").GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("obstacle"))
        {
            TakeDamage(1, 8f, 0.2f); // Example values, adjust as needed
        }
    }

    void TakeDamage(int damage, float intensity, float time)
    {
        currentHealth -= damage;

        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(intensity, time);
        }

        if (audioSource != null && hurtSound != null)
        {
            // Play hurt sound
            audioSource.PlayOneShot(hurtSound);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Disable the player GameObject or perform any other death-related actions
        gameObject.SetActive(false);

        if (audioSource != null && explosionSound != null)
        {
            // Play explosion sound
            audioSource.PlayOneShot(explosionSound);
        }

        Debug.Log("Player died!");
    }
}
