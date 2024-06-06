using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public CMShake cameraShake; // Reference to the CameraShake script
    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip hurtSound; // Sound to play when player is hurt
    public AudioClip explosionSound; // Sound to play when player dies
    public AudioClip gameOverSound;

    public GameObject[] healthObjects; // Array to hold the health-related game objects
    public GameObject deathEffectPrefab; // Prefab for death effect
    public float deathEffectSpeed = 5.0f; // Speed at which death effect object moves
    public GameObject layerObject; // Reference to another GameObject whose order in layer will be passed to the death effect object

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GameObject.FindGameObjectWithTag("audio").GetComponentInChildren<AudioSource>();

        // Ensure all health objects are initially invisible
        foreach (GameObject obj in healthObjects)
        {
            SetObjectVisibility(obj, false);
        }
        UpdateHealthObject();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("obstacle"))
        {
            TakeDamage(1, 8f, 0.2f); // Example values, adjust as needed
        }
        if (other.CompareTag("Bomb"))
        {
            TakeDamage(3, 16f, 0.5f); // Example values, adjust as needed
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
        else
        {
            // Update active health object based on current health
            UpdateHealthObject();
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
            audioSource.PlayOneShot(gameOverSound);
        }

        Debug.Log("Player died!");

        // Calculate spawn position with offset
        Vector3 spawnPosition = transform.position + new Vector3(-0.25f, 1.3f, 0f);

        // Get order in layer of the other GameObject
        int orderInLayer = 0; // Default value
        if (layerObject != null)
        {
            SpriteRenderer otherRenderer = layerObject.GetComponent<SpriteRenderer>();
            if (otherRenderer != null)
            {
                orderInLayer = otherRenderer.sortingOrder;
            }
        }

        // Spawn death effect at the calculated position and move it to the left
        GameObject deathEffect = Instantiate(deathEffectPrefab, spawnPosition, Quaternion.identity);
        deathEffect.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer; // Set order in layer
    }

    void UpdateHealthObject()
    {
        // Ensure all health objects are invisible
        for (int i = 0; i < healthObjects.Length; i++)
        {
            SetObjectVisibility(healthObjects[i], false);
        }

        // Set visibility based on current health
        if (currentHealth >= 3)
        {
            SetObjectVisibility(healthObjects[0], true);
        }
        else if (currentHealth == 2)
        {
            SetObjectVisibility(healthObjects[1], true);
        }
        else if (currentHealth == 1)
        {
            SetObjectVisibility(healthObjects[2], true);
            SetObjectVisibility(healthObjects[3], true); // 4th object visible only at 1 health
        }
    }

    void SetObjectVisibility(GameObject obj, bool visible)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = visible;
        }
    }
}
