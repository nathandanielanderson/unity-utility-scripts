using UnityEngine;

public class CoinBurstController : MonoBehaviour
{
    [Header("Coin Burst Settings")]
    public int numberOfCoins = 30; // Control the number of coins via this field
    private ParticleSystem particleSystem;

    private void Awake()
    {
        // Get the Particle System component
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("No ParticleSystem component found on this GameObject.");
        }
    }

    public void Start()
    {
        // Burst the coins when the GameObject is enabled
        BurstCoins();
    }

    public void BurstCoins()
    {
        if (particleSystem == null) return;

        // Access the Emission Module of the Particle System
        ParticleSystem.EmissionModule emission = particleSystem.emission;

        // Set the burst count dynamically
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, numberOfCoins);
        emission.SetBursts(new ParticleSystem.Burst[] { burst });

        // Play the particle system
        particleSystem.Play();
    }

    public void SetNumberOfCoins(int coinCount)
    {
        // Update the number of coins dynamically
        numberOfCoins = Mathf.Max(0, coinCount); // Ensure it's non-negative
    }
}