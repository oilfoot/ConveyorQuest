using UnityEngine;

public class SmokeParticleController : MonoBehaviour
{
    // Public variable to control the emission rate multiplier
    public int emissionRateMultiplier = 5;

    // Reference to the ParticleSystem component
    private ParticleSystem myParticleSystem;

    void Start()
    {
        // Get the ParticleSystem component attached to the GameObject
        myParticleSystem = GetComponent<ParticleSystem>();

        // Start the coroutine to destroy the GameObject after 15 seconds
        StartCoroutine(DestroyAfterTime(15f));

        // Set the emission rate based on the multiplier
        SetEmissionRate(emissionRateMultiplier);
    }

    public void SetEmissionRate(int multiplier)
    {
        // Calculate the emission rate
        float emissionRate = multiplier * 10;

        // Get the ParticleSystem's emission module
        var emission = myParticleSystem.emission;

        // Set the rate over time to the calculated value
        emission.rateOverTime = emissionRate;
    }

    System.Collections.IEnumerator DestroyAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
