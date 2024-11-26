using UnityEngine;
using Mirror;

public class CoinBurstSpawner : NetworkBehaviour
{
    [Header("Coin Burst Settings")]
    public GameObject coinBurstPrefab; // Prefab for the coin burst effect

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Ensure this logic only runs on the server
        if (isServer)
        {
            // Spawn the coin burst effect at (0, 0, 0)
            Vector3 spawnPosition = Vector3.zero;
            GameObject spawned = Instantiate(coinBurstPrefab, spawnPosition, Quaternion.identity);
            NetworkServer.Spawn(spawned);
        }
    }
}