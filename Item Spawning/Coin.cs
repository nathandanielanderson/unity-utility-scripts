using UnityEngine;
using Mirror;
using System;

public class Coin : NetworkBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerInteractions component
            PlayerInteractions interactions = other.GetComponent<PlayerInteractions>();

            if (interactions != null)
            {
                // Call the Pickup method on the PlayerInteractions component
                if (isServer)
                {
                    interactions.Pickup(gameObject);
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