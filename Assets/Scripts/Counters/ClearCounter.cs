using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // No Kitchen Object on the Counter
        if (!HasKitchenObject())
        {
            // Player carrying Kitchen Object
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        // Kitchen Object On the Counter
        else
        {
            // Player not carrying Kitchen Object
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            // Player is carrying kitchen Object
            else
            {
                // Kitchen object being carried is a plate, put what is on the counter on the plate
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject carriedPlateKitchenObject))
                {
                    if (carriedPlateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }   
                }
                // Kitchen Object being carried is not a plate
                else
                {
                    // Counter has plate on it, put Kitchen object player is holding on the plate
                    if(GetKitchenObject().TryGetPlate(out PlateKitchenObject counterPlateKitchenObject))
                    {
                        if(counterPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
}
