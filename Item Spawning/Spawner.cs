using UnityEngine;
using Mirror;

public class Spawner : NetworkBehaviour
{
    public GameObject spawnablePrefab; // Assign in the Inspector

    private void Start()
    {
        // Ensure this logic runs only on the server
        if (isServer && spawnablePrefab != null)
        {
            // Spawn the prefab on the server
            GameObject spawned = Instantiate(spawnablePrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(spawned);

            // Destroy the spawner object on the server
            NetworkServer.Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}