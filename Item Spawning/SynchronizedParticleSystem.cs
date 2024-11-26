using UnityEngine;
using Mirror;

public class SynchronizedParticleSystem : NetworkBehaviour
{
    [Header("Particle System to Sync")]
    public ParticleSystem particleSystem;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Trigger the particle system only on the server
        if (isServer)
        {
            StartParticleSystem();
        }
    }

    private void StartParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.Clear(); // Clear existing particles
            particleSystem.Play();  // Start the particle system
        }
    }

    private void StopParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.Stop(); // Stop the particle system
        }
    }
}