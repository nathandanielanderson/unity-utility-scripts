using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class GroundSpawner : MonoBehaviour
{
    public GameObject coinSpawnerPrefab; // Assign the CoinSpawner prefab in the Inspector

    private void OnParticleCollision(GameObject particleSystemObject)
    {
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        if (particleSystem == null) return;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, gameObject, collisionEvents);

        foreach (var collisionEvent in collisionEvents)
        {
            Vector3 spawnPosition = collisionEvent.intersection;
            Instantiate(coinSpawnerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}