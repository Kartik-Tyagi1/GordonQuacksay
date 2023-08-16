using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public GameObject gameObject;
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectsSO_GameObjects;

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
        foreach (KitchenObjectSO_GameObject obj in kitchenObjectsSO_GameObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddIngredientEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject obj in kitchenObjectsSO_GameObjects)
        {
            if(obj.kitchenObjectSO == e.kitchenObjectSO)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
