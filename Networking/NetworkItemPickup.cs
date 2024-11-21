using Mirror;
using UnityEngine;

public class NetworkItemPickup : NetworkBehaviour
{
    public void PickupItemRequest()
    {
        // Ensure this runs on the server to synchronize across clients
        if (isServer)
        {
            RpcDestroyItem();
        }
    }

    [ClientRpc]
    void RpcDestroyItem()
    {
        Destroy(gameObject);
    }
}