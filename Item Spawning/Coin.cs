using UnityEngine;
using Mirror;
using System;

public class Coin : NetworkBehaviour
{
    public AudioClip pickupSound; // Assign the coin pickup sound in the Inspector

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerInteractions component
            PlayerInteractions interactions = other.GetComponent<PlayerInteractions>();

            if (interactions != null)
            {
                // Handle the pickup logic
                if (isServer)
                {
                    interactions.Pickup(gameObject);
                }

                // Play sound locally for the player
                if (interactions.isLocalPlayer)
                {
                    interactions.PlayPickupSound(pickupSound);
                }
            }
        }
    }

    // Static event to notify when a coin is destroyed
    public static event Action<Coin> OnCoinDestroyed;

    private void OnDestroy()
    {
        // Invoke the event when the coin is destroyed
        OnCoinDestroyed?.Invoke(this);
    }
}