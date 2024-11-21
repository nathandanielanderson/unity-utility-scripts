using Mirror;
using UnityEngine;

namespace Invector.vCharacterController.vActions
{
    public class NetworkGenericAction : vGenericAction
    {
        public override void TriggerActionEvents()
        {
            base.TriggerActionEvents();

            // Get the NetworkItemPickup component from the item
            NetworkItemPickup networkItem = triggerAction.GetComponent<NetworkItemPickup>();
            if (networkItem != null)
            {
                // Directly request the item pickup without authority checks
                networkItem.PickupItemRequest();
            }
        }
    }
}