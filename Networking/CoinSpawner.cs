using UnityEngine;
using Mirror;

public class CoinSpawner : NetworkBehaviour
{
    public GameObject coinPrefab; // Assign your Pickup prefab in the inspector
    public float spawnInterval = 5f; // Time between spawns (set to 0 for single spawn)
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // Define the spawn area

    private void Start()
    {
        if (isServer)
        {
            if (spawnInterval > 0)
            {
                // Repeated spawning
                InvokeRepeating(nameof(SpawnCoin), 1f, spawnInterval);
            }
            else
            {
                // Single spawn
                SpawnCoin();
            }
        }
    }

    [Server]
    private void SpawnCoin()
    {
        // Random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            1f, // Height
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Instantiate and spawn the coin
        GameObject coin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        NetworkServer.Spawn(coin);
    }
}