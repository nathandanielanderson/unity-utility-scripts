using UnityEngine;
using Mirror;

public class CoinShower : NetworkBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab; // Assign your Coin prefab in the Inspector
    public Vector3 spawnAreaSize = new Vector3(10, 1, 10); // Size of the area to spawn coins
    public float spawnHeight = 10f; // Height from which the coins will fall

    [Header("Spawning Controls")]
    public int coinsPerInterval = 5; // How many coins to spawn each interval
    public float spawnInterval = 2f; // Interval in seconds between spawns

    private bool spawning = false;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Start the spawning process on the server
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawning) return; // Avoid duplicate coroutines
        spawning = true;
        StartCoroutine(SpawnCoinsOverTime());
    }

    private System.Collections.IEnumerator SpawnCoinsOverTime()
    {
        while (spawning)
        {
            SpawnCoins();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("Coin prefab is not assigned.");
            return;
        }

        for (int i = 0; i < coinsPerInterval; i++)
        {
            // Generate a random position within the spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                spawnHeight,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            // Spawn the coin on the server
            if (isServer)
            {
                GameObject spawnedCoin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);
                NetworkServer.Spawn(spawnedCoin); // Sync with all clients
            }
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}