using UnityEngine;
using System.Collections.Generic;

public class ParticleCollisionHandler : MonoBehaviour
{
    public GameObject spawnerPrefab; // Assign your spawner prefab here.

    private ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        // Get all collision events for this particle system
        int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(particleSystem, other, collisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            // Get the position of the collision
            Vector3 collisionPosition = collisionEvents[i].intersection;

            // Instantiate the spawner prefab at the collision position
            Instantiate(spawnerPrefab, collisionPosition, Quaternion.identity);
        }
    }
}