using UnityEngine;
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure there's an AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    // Called when the player picks up an item
    public void Pickup(GameObject item)
    {
        Debug.Log($"Picked up: {item.name}");

        // Destroy the item after picking it up
        Destroy(item);

        // Add additional logic here, such as updating inventory or UI
    }

    // Method to play a sound only for the local player
    public void PlayPickupSound(AudioClip clip)
    {
        if (clip != null && isLocalPlayer)
        {
            // Create a temporary audio source for overlapping playback
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.clip = clip;
            tempSource.volume = 0.8f; // Set a base volume

            // Play the sound and destroy the temporary audio source after it finishes
            tempSource.Play();
            Destroy(tempSource, clip.length);
        }
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