using UnityEngine;
using Mirror;

public class CoinSpawner : NetworkBehaviour
{
    public GameObject coinPrefab; // Assign your Pickup prefab in the inspector
    public float spawnInterval = 5f; // Time between spawns
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // Define the spawn area

    private void Start()
    {
        if (isServer)
        {
            InvokeRepeating(nameof(SpawnCoin), 1f, spawnInterval);
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