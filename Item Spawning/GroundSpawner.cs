using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class GroundSpawner : NetworkBehaviour
{
    public GameObject spawnerPrefab; // Assign your spawner prefab here.
    public ParticleSystem particleSystem; // Assign the ParticleSystem in the Inspector.
    public int maxSpawnedObjects = 10; // Maximum number of spawned objects allowed.
    public float emissionRate = 1.5f; // Rate at which particles are emitted.

    private List<Coin> spawnedCoins = new List<Coin>(); // List to track all spawned coins

    private void Start()
    {
        // Listen for coin destruction events
        Coin.OnCoinDestroyed += HandleCoinDestroyed;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the spawner is destroyed
        Coin.OnCoinDestroyed -= HandleCoinDestroyed;
    }

    private void OnParticleCollision(GameObject particleSystemObject)
    {
        // Ensure this logic runs only on the server
        if (!isServer) return;

        // Ensure the particle system is the one assigned to this script
        if (particleSystemObject.GetComponent<ParticleSystem>() != particleSystem) return;

        // Create a list to store collision events
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        // Retrieve all collision events
        int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, gameObject, collisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            if (spawnedCoins.Count >= maxSpawnedObjects)
            {
                SetEmissionRate(0); // Stop emitting new particles
                return;
            }

            // Get the position of the collision
            Vector3 collisionPosition = collisionEvents[i].intersection;

            // Instantiate the spawner prefab at the collision position
            GameObject spawned = Instantiate(spawnerPrefab, collisionPosition, Quaternion.identity);
            NetworkServer.Spawn(spawned);

            // Add the coin to the list
            Coin coin = spawned.GetComponent<Coin>();
            if (coin != null)
            {
                spawnedCoins.Add(coin);
            }
        }
    }

    private void HandleCoinDestroyed(Coin coin)
    {
        // Remove the coin from the list when it's destroyed
        spawnedCoins.Remove(coin);

        // Resume emission if the count is below the max limit
        if (spawnedCoins.Count < maxSpawnedObjects)
        {
            SetEmissionRate(emissionRate);
        }
    }

    private void SetEmissionRate(float rate)
    {
        if (particleSystem != null)
        {
            ParticleSystem.EmissionModule emission = particleSystem.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(rate);
        }
    }
}