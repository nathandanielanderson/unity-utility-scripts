using UnityEngine;
using Mirror;
using System;

public class Coin : NetworkBehaviour
{
    [SerializeField] private AudioClip pickupSound; // Assign the coin pickup sound in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerInteractions component from the player
            PlayerInteractions player = other.GetComponent<PlayerInteractions>();

            if (player != null)
            {
                HandlePickup(player);
            }
        }
    }

    private void HandlePickup(PlayerInteractions player)
    {
            // Server handles the game state (destroying the coin, updating coin count)
            if (player.isLocalPlayer)
            {
                // Play the pickup sound for the local player
                player.PlayPickupSound(pickupSound);
            }
            
            player.Pickup(gameObject);
            

    }

    // Static event to notify when a coin is destroyed
    public static event Action<Coin> OnCoinDestroyed;

    private void OnDestroy()
    {
        // Invoke the event when the coin is destroyed
        OnCoinDestroyed?.Invoke(this);
    }
}