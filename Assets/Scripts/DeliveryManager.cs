using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    // Contains a list of the recipies
    [SerializeField] private RecipieListSO recipieListSO;

    private List<RecipieSO> waitingRecipieSOList;
    private float spawnRecipieTimer;
    private float spawnRecipieTimerMax = 4f;
    private int watingRecipiesMax = 4;

    public event EventHandler OnRecepieCreated;
    public event EventHandler OnRecepieRemoved;

    private void Awake()
    {
        Instance = this;
        waitingRecipieSOList = new List<RecipieSO>();
    }

    private void Update()
    {
        spawnRecipieTimer -= Time.deltaTime;
        if(spawnRecipieTimer <= 0f )
        {
            spawnRecipieTimer = spawnRecipieTimerMax;

            if(waitingRecipieSOList.Count < watingRecipiesMax )
            {
                RecipieSO waitingRecipieSO = recipieListSO.recipieSOList[UnityEngine.Random.Range(0, recipieListSO.recipieSOList.Count)];
                //Debug.Log(waitingRecipieSO.recipieName);
                waitingRecipieSOList.Add(waitingRecipieSO);
                OnRecepieCreated?.Invoke(this, EventArgs.Empty); 
            }
        }
    }

    public void CheckDelivery(PlateKitchenObject plate)
    {
        for(int i = 0; i < waitingRecipieSOList.Count; i++)
        {
            RecipieSO watingRecipieSO = waitingRecipieSOList[i];

            // Count of ingredients on recipie match count of ingredients on the plate
            if(watingRecipieSO.kitchenObjectSOList.Count == plate.GetKitchenObjectSOList().Count)
            {
                bool recipieMatches = true;

                // Look through each recepie and the plate to see if the ingredients match
                foreach (KitchenObjectSO recepieKitchenObjectSO in watingRecipieSO.kitchenObjectSOList)
                {
                    bool ingredientMatches = false;
                    foreach(KitchenObjectSO deliveryKitchenObjectSO in plate.GetKitchenObjectSOList())
                    {
                        // If an ingredient on the plate matches an ingredient on the recpie, break out and move on to check the next one
                        if(deliveryKitchenObjectSO == recepieKitchenObjectSO)
                        {
                            ingredientMatches = true;
                            break;
                        }
                    }

                    // If no ingredient on the plate matches the ingredient on the recepie, then this recepie does not match the plate
                    if(!ingredientMatches) 
                    {
                        recipieMatches = false;
                    }
                }

                // If the ingredients on the plate matches the ingredient on the recepie, then this recepie is correct
                // Otherwise move on to the next recepie
                if (recipieMatches)
                {
                    //Debug.Log("Player Delivered Correct Recipie");
                    waitingRecipieSOList.RemoveAt(i);
                    OnRecepieRemoved?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        //Debug.Log("Player Did Not Deliver the Correct Recipie");
    }

    public List<RecipieSO> GetWaitingRecipieSOList() { return waitingRecipieSOList; }
}
