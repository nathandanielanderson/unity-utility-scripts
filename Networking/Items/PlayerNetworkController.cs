using UnityEngine;
using Mirror;

public class PlayerNetworkController : NetworkBehaviour
{
    [ClientRpc]
    public void RpcOnItemPickup(GameObject item)
    {
        // Client-side logic for when an item is picked up
        Debug.Log($"Item {item.name} picked up!");
        // Example: Play particle effects or sounds here
    }
}