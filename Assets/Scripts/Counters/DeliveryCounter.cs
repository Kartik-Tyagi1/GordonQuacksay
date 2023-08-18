using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        // Destroy the plate the player is carrying since delivery counter should only accept plates
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.CheckDelivery(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
