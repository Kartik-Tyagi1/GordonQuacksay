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
        // Transform is not just object location and position, it is basically the entire game object
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        kitchenObjectTransform.localPosition = Vector3.zero;
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

}
