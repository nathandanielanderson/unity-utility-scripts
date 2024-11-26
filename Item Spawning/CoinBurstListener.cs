using UnityEngine;
using Mirror;

public class CoinBurstListener : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private int lastPlayerCount = 0; // Track the number of players currently connected

    private void Awake()
    {
        // Get the ParticleSystem component
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("No ParticleSystem found on this GameObject.");
        }
    }

    private void Update()
    {
        if (!NetworkServer.active) return; // Only run this logic on the server

        int currentPlayerCount = NetworkServer.connections.Count; // Count current connections
        if (currentPlayerCount > lastPlayerCount)
        {
            // A new player has joined
            OnPlayerLogin();
        }

        // Update the last known player count
        lastPlayerCount = currentPlayerCount;
    }

    private void OnPlayerLogin()
    {
        // Trigger the particle system when a player logs in
        if (particleSystem != null)
        {
            particleSystem.Clear(); // Clear existing particles
            particleSystem.Play();  // Start the particle system
        }
    }
}