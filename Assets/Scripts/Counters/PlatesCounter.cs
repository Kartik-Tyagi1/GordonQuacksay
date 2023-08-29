using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private float spawnPlateTimerMax = 4f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if(GameHandler.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax) 
            {
                platesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        // Player is emtpy handed and we have plates available
        if(!player.HasKitchenObject() && platesSpawnedAmount > 0)
        {
            platesSpawnedAmount--;
            KitchenObject.CreateKitchenObjectAndAssignParent(plateKitchenObjectSO, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
