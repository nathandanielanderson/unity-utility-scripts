using UnityEngine;
using TMPro; // Required for TextMeshPro
using Mirror;

public class PlayerInteractions : NetworkBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private GameObject coinIcon; // Reference to the Coin Icon parent
    [SerializeField] private TextMeshProUGUI coinCountText; // Reference to the TextMeshPro component for coin count
    private int coinCount = 0; // Tracks the player's coin count

    private void Awake()
    {
        // Ensure there's an AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // Ensure the coin icon starts disabled
        if (coinIcon != null)
        {
            coinIcon.SetActive(false);
        }
    }

    private void Start()
    {
        // Initialize the coin count UI for the local player only
        if (isLocalPlayer)
        {
            UpdateCoinUI();
        }
    }

    // Called when the player picks up an item
    public void Pickup(GameObject item)
    {
        if (!isLocalPlayer) return; // Ensure this only happens for the local player

        Debug.Log($"Picked up: {item.name}");

        // Check if the item is a coin
        if (item.CompareTag("Coin"))
        {
            // Enable the coin icon if it is currently disabled
            if (coinIcon != null && !coinIcon.activeSelf)
            {
                coinIcon.SetActive(true);
            }

            // Increment the coin count
            coinCount++;

            // Update the UI
            UpdateCoinUI();

            // Optionally, play a pickup sound for the local player
            PlayPickupSound(item.GetComponent<Coin>().pickupSound); // Assuming Coin prefab has a pickupSound field
        }

        // Destroy the item after picking it up (handled on the server)
        if (isServer)
        {
            NetworkServer.Destroy(item);
        }
    }

    // Updates the coin count in the UI
    private void UpdateCoinUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = coinCount.ToString();
        }
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
        if (!isLocalPlayer) return; // Ensure this only happens for the local player

        Debug.Log($"Dropped: {itemPrefab.name}");

        // Instantiate the item at the drop position
        if (isServer)
        {
            GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            NetworkServer.Spawn(droppedItem);
        }
    }
}