using UnityEngine;
using System.Collections;

public class DestroyCameraOnLogout : MonoBehaviour
{
    public Transform mainTarget; // Assign the target in the Inspector or dynamically

    void Start()
    {
        // Start a delayed check to prevent immediate destruction
        StartCoroutine(CheckTargetAfterDelay());
    }

    private IEnumerator CheckTargetAfterDelay()
    {
        // Wait for 0.5 seconds to allow initialization of the target
        yield return new WaitForSeconds(0.5f);

        // Check if mainTarget is still null
        if (mainTarget == null)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Continuously check for the target; destroy if it goes missing during runtime
        if (mainTarget == null)
        {
            Destroy(gameObject);
        }
    }
}