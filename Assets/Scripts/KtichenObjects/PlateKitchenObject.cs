using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    public event EventHandler<OnAddIngredientEventArgs> OnAddIngredient;
    public class OnAddIngredientEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    // Ingredients on the plate
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // Not valid ingredient
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnAddIngredient?.Invoke(this, new OnAddIngredientEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;
        }

    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() { return kitchenObjectSOList; }
}
