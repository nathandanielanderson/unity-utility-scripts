using UnityEngine;
using Mirror;

public class SpawnHandler : MonoBehaviour
{
    public GameObject spawnablePrefab; // Assign in Inspector

    void Start()
    {
        // Register custom spawn and unspawn handlers
        NetworkClient.RegisterPrefab(
            spawnablePrefab,
            SpawnItem, // Spawn handler
            UnspawnItem // Unspawn handler
        );
    }

    // Custom spawn handler
    public GameObject SpawnItem(SpawnMessage msg)
    {
        return Instantiate(spawnablePrefab, msg.position, msg.rotation);
    }

    // Custom unspawn handler
    public void UnspawnItem(GameObject obj)
    {
        Destroy(obj);
    }
}