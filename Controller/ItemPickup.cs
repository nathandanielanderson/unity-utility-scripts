using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;

                    // Check if the player has a PlayerSetup component
                    PlayerSetup playerSetup = GetComponent<PlayerSetup>();
                    if (playerSetup != null)
                    {
                        playerSetup.TryPickupItem(hitObject);
                    }
                }
            }
        }
    }
}