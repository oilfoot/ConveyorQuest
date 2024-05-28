using UnityEngine;

public class SpawnPointChecker : MonoBehaviour
{
    public SpawnController spawnController;
    public ParticleSystem particleEffectPrefab1; // First particle effect prefab
    public ParticleSystem particleEffectPrefab2; // Second particle effect prefab
    public Transform[] particleSpawnPoints;
    public float particleMoveSpeed = 1f; // Adjustable speed value

    public float particleSpawnInterval = 2f;
    private float nextParticleSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the next particle spawn time
        nextParticleSpawnTime = Time.time + particleSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpawnPoints();
    }

    private void CheckSpawnPoints()
    {
        for (int i = 0; i < spawnController.spawnPoints.Length; i++)
        {
            if (!spawnController.spawnPoints[i].isActive)
            {
                Debug.Log($"Element {i + 1} Inactive");

                // Spawn particle effects at the corresponding location every 2 seconds
                if (Time.time >= nextParticleSpawnTime)
                {
                    SpawnParticleEffectAt(i);
                    nextParticleSpawnTime = Time.time + particleSpawnInterval;
                }
            }
        }
    }

    private void SpawnParticleEffectAt(int index)
    {
        if (index >= 0 && index < particleSpawnPoints.Length)
        {
            // Instantiate the first particle effect with a rotation of -90 degrees on the x-axis
            ParticleSystem particleInstance1 = Instantiate(particleEffectPrefab1, particleSpawnPoints[index].position, Quaternion.Euler(-90, 0, 0));
            // Instantiate the second particle effect with a rotation of -90 degrees on the x-axis
            ParticleSystem particleInstance2 = Instantiate(particleEffectPrefab2, particleSpawnPoints[index].position, Quaternion.Euler(-90, 0, 0));

            // Add a component to move the first particle
            ParticleMover mover1 = particleInstance1.gameObject.AddComponent<ParticleMover>();
            mover1.speed = particleMoveSpeed;

            // Add a component to move the second particle
            ParticleMover mover2 = particleInstance2.gameObject.AddComponent<ParticleMover>();
            mover2.speed = particleMoveSpeed;
        }
        else
        {
            Debug.LogWarning("Particle spawn point index out of range!");
        }
    }

    private class ParticleMover : MonoBehaviour
    {
        public float speed;

        void Update()
        {
            // Move the particle to the left
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
