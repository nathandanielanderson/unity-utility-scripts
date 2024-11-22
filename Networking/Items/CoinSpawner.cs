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
        Vector3 spawnPosition;

        // Check if the spawn area size is (0, 0, 0)
        if (spawnAreaSize == Vector3.zero)
        {
            // Spawn at the spawner object's origin
            spawnPosition = transform.position;
        }
        else
        {
            // Spawn at a random position within the defined spawn area
            spawnPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                1f, // Height
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            ) + transform.position; // Offset by the spawner's position
        }

        // Instantiate and spawn the coin
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(coin);
    }
}