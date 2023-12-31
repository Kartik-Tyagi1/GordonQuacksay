using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    private KitchenObject kitchenObject;
    [SerializeField] protected Transform counterTopPoint;

    public static event EventHandler OnKitchenObjectPlacedOnCounter;

    public virtual void Interact(Player player) 
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null )
        {
            OnKitchenObjectPlacedOnCounter?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() { return kitchenObject != null; }

    public static void ResetStaticData()
    {
        OnKitchenObjectPlacedOnCounter = null;
    }

}
