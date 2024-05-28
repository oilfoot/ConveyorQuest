using UnityEngine;

public class EmissionController : MonoBehaviour
{
    // Reference to the SmokeParticleController script
    public SmokeParticleController smokeController;
    public float DecreaseEmissionTime;

    // To keep track of whether the player is in the trigger
    public bool playerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the tag "player"
        if (other.CompareTag("player"))
        {
            // Set the flag to true and start the coroutine
            playerInTrigger = true;
            StartCoroutine(DecreaseEmissionRate());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the trigger has the tag "player"
        if (other.CompareTag("player"))
        {
            // Set the flag to false to stop the coroutine
            playerInTrigger = false;
        }
    }

    private System.Collections.IEnumerator DecreaseEmissionRate()
    {
        while (playerInTrigger)
        {
            // Wait for 0.5 seconds
            yield return new WaitForSeconds(DecreaseEmissionTime);

            // Decrease the emission rate multiplier and update the emission rate
            if (smokeController != null)
            {
                smokeController.emissionRateMultiplier -= 1;
                smokeController.SetEmissionRate(smokeController.emissionRateMultiplier);
            }
        }
    }
}
