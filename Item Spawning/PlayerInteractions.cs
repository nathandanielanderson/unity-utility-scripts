using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Called when the player picks up an item
    public void Pickup(GameObject item)
    {
        Debug.Log($"Picked up: {item.name}");

        // Destroy the item after picking it up
        Destroy(item);

        // Add additional logic here, such as updating inventory or UI
    }

    // Called when the player drops an item
    public void Drop(GameObject itemPrefab, Vector3 dropPosition)
    {
        Debug.Log($"Dropped: {itemPrefab.name}");

        // Instantiate the item at the drop position
        Instantiate(itemPrefab, dropPosition, Quaternion.identity);

        // Add additional logic here, such as removing the item from inventory
    }
}