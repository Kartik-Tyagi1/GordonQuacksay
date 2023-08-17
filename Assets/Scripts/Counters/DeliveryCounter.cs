using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // Destroy the plate the player is carrying since delivery counter should only accept plates
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
