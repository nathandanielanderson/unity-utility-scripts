using UnityEngine;
using Mirror;

public class ItemSpawner : NetworkBehaviour
{
    public GameObject itemPrefab; // Assign in Inspector
    
    void OnStart()
    {
        CmdSpawnItem(new Vector3(0, 0, 0));
    }

    [Command]
    public void CmdSpawnItem(Vector3 position)
    {
        if (!isServer) return;

        // Instantiate the item on the server
        GameObject item = Instantiate(itemPrefab, position, Quaternion.identity);

        // Spawn it across all clients
        NetworkServer.Spawn(item);
    }
}