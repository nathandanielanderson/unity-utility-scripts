using Mirror;
using UnityEngine;

public class NetworkItemSpawner : MonoBehaviour
{
    [Header("Item Spawning Settings")]
    public GameObject itemPrefab; // The item or pickup prefab to spawn
    public int initialSpawnCount = 10; // Number of items to spawn initially
    public float spawnInterval = 10f; // Time interval between spawns (seconds)

    [Header("Spawn Area")]
    public Vector3 spawnAreaMin = new Vector3(-10, 0, -10); // Minimum bounds for spawning
    public Vector3 spawnAreaMax = new Vector3(10, 0, 10);   // Maximum bounds for spawning

    private bool isSpawning = false; // Tracks whether the spawner is running

    private void Start()
    {
        // Ensure this logic only runs on the server
        if (NetworkServer.active)
        {
            StartSpawning();
        }
    }

    [Server]
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;

            // Initial spawn
            for (int i = 0; i < initialSpawnCount; i++)
            {
                SpawnItem();
            }

            // Start periodic spawning
            InvokeRepeating(nameof(SpawnItem), spawnInterval, spawnInterval);
        }
    }

    [Server]
    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke(nameof(SpawnItem));
        }
    }

    [Server]
    private void SpawnItem()
    {
        // Generate a random position within the spawn area bounds
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        // Instantiate the item prefab at the random position
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

        // Spawn the item across the network
        NetworkServer.Spawn(item);
    }
}