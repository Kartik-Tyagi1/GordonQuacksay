using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;

    public override void Interact(Player player)
    {
        // No Kitchen Object on the Counter
        if (!HasKitchenObject())
        {
            // Player carrying Kitchen Object
            // Only drop objects that can be cut
            if (player.HasKitchenObject() && HasRecipieForInput(player.GetKitchenObject().GetKitchenObjectSO()))
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
            KitchenObjectSO output = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            if (output)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.CreateKitchenObjectAndAssignParent(output, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray)
        {
            if (cuttingRecipieSO.input == inputkitchenObjectSO)
            {
                return cuttingRecipieSO.output;
            }
        }

        return null;
    }

    private bool HasRecipieForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray)
        {
            if (cuttingRecipieSO.input == inputkitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }
}
