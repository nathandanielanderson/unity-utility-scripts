using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class GroundSpawner : NetworkBehaviour
{
    
    public GameObject coinPrefab; // Assign the CoinSpawner prefab in the Inspector

    private void OnParticleCollision(GameObject particleSystemObject)
    {
        if(!isServer) return;
        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        if (particleSystem == null) return;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, gameObject, collisionEvents);

        foreach (var collisionEvent in collisionEvents)
        {
            Vector3 spawnPosition = collisionEvent.intersection;
            // Instantiate the spawner prefab at the collision position
            GameObject spawned = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            NetworkServer.Spawn(spawned);
        }
    }
}