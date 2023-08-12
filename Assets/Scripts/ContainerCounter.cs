using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    // Container counters will dispense things like tomatos, cheese, etc. directly to player
    public override void Interact(Player player)
    {
        // Only give kitchen object to player if player is not carrying one
        if(!player.HasKitchenObject())
        {
            KitchenObject.CreateKitchenObjectAndAssignParent(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
