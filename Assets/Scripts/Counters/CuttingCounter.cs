using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipieSO[] cuttingRecipieSOArray;
    private int cuttingProgress;

    // OnAnyCut used for sound effect
    public static event EventHandler OnAnyCut;

    // OnCut used for animation
    public event EventHandler OnCut;

    // OnProgressChanged uses for UI progress bar
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
                cuttingProgress = 0;
                OnProgressChanged?.Invoke(
                    this,
                    new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0.00001f }
                ); 
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
                // Kitchen object is a plate, put what is on the counter on the plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        // Kitchen Object on the Counter
        if (HasKitchenObject())
        {
            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipie(GetKitchenObject().GetKitchenObjectSO());
            if (cuttingRecipieSO == null) return;

            int cuttingProgressMax = cuttingRecipieSO.cuttingProgressMax;

            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(
                this, 
                new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingProgressMax }
            );

            if (cuttingProgress >= GetCuttingRecipie(GetKitchenObject().GetKitchenObjectSO())?.cuttingProgressMax) 
            {
                KitchenObjectSO output = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                if (output)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.CreateKitchenObjectAndAssignParent(output, this);
                }
            }
        }
    }

    /*
    * Returns the corresponding CuttingRecipieSO for the input KitchenObjectSO
    */
    private CuttingRecipieSO GetCuttingRecipie(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray)
        {
            if (cuttingRecipieSO.input == inputkitchenObjectSO)
            {
                return cuttingRecipieSO;
            }
        }

        return null;
    }

    /*
     * Returns the output KitchenObjectSO from the CuttingRecipieSO given the input KitchenObjectSO
     */
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        return GetCuttingRecipie(inputkitchenObjectSO) != null ? GetCuttingRecipie(inputkitchenObjectSO).output : null;
    }

    private bool HasRecipieForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        return GetCuttingRecipie(inputkitchenObjectSO) != null;
    }
}
