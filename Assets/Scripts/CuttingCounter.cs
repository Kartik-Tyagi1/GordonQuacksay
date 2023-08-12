using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

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
        }
    }

    public override void InteractAlternate(Player player)
    {
        // Kitchen Object on the Counter
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
            
            KitchenObject.CreateKitchenObjectAndAssignParent(cutKitchenObjectSO, this);
        }
    }
}
