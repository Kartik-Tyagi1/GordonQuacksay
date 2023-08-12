using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;

    public static KitchenObject CreateKitchenObjectAndAssignParent(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform cutKitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        cutKitchenObjectTransform.localPosition = Vector3.zero;

        KitchenObject kitchenObject = cutKitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent in_kitchenObjectParent) 
    {
        // Before we reparent the kitchen object, remove it from the old parent
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = in_kitchenObjectParent;

        if(!in_kitchenObjectParent.HasKitchenObject())
        {
            this.kitchenObjectParent.SetKitchenObject(this);
        }
        else
        {
            Debug.LogError("Kitchen Object Parent already has kitchen object");
        }

        // When we reparent the kitchenObject we have to set its new location to the top point of the parent
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() { return kitchenObjectParent; }

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
