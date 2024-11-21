using UnityEngine;
using Mirror;

public class Coin : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnCoinSetup))]
    public int coinValue; // Value of the coin, synced to all clients

    public GameObject coinVisual; // Visual representation of the coin

    // Hook called when coinValue changes
    private void OnCoinSetup(int oldValue, int newValue)
    {
        UpdateVisual(newValue);
    }

    private void UpdateVisual(int value)
    {
        // Example: Change material or size based on coin value
        // Add your custom logic for updating visuals here
    }
}