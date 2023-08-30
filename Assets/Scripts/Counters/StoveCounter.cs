using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static CuttingCounter;
using static Player;

public class StoveCounter : BaseCounter, IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt
    }
    private State state;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipieSO[] burningRecipieSOArray;

    FryingRecipieSO fryingRecipieSO;
    BurningRecipieSO burningRecipieSO;

    private float fryingTime;
    private float burningTime;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTime += Time.deltaTime;
                    OnProgressChanged(
                        this,
                        new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTime / fryingRecipieSO.fryingTimerMax }
                    );

                    if (fryingTime > fryingRecipieSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.CreateKitchenObjectAndAssignParent(fryingRecipieSO.output, this);
                        SetState(State.Fried);

                        burningTime = 0f;
                        burningRecipieSO = GetBurningRecipieSO(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    burningTime += Time.deltaTime;
                    OnProgressChanged(
                        this,
                        new IHasProgress.OnProgressChangedEventArgs { progressNormalized = burningTime / burningRecipieSO.burningTimerMax }
                    );

                    if (burningTime > burningRecipieSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.CreateKitchenObjectAndAssignParent(burningRecipieSO.output, this);
                        SetState(State.Burnt);

                        OnProgressChanged(
                            this,
                            new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f }
                        );
                    }
                    break;
                case State.Burnt:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        // No Kitchen Object on the Counter
        if (!HasKitchenObject())
        {
            // Player carrying Kitchen Object
            // Only drop objects that can be fried
            if (player.HasKitchenObject() && HasRecipieForInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipieSO = GetFryingRecipieSO(GetKitchenObject().GetKitchenObjectSO());
                SetState(State.Frying);
                fryingTime = 0f;

                OnProgressChanged(
                    this,
                    new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTime / fryingRecipieSO.fryingTimerMax }
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
                SetState(State.Idle);
                OnProgressChanged(
                    this,
                    new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f }
                );
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
                        SetState(State.Idle);
                        OnProgressChanged(
                            this,
                            new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f }
                        );
                    }
                }
            }
        }
    }

    /*
    * Returns the corresponding CuttingRecipieSO for the input KitchenObjectSO
    */
    private FryingRecipieSO GetFryingRecipieSO(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (FryingRecipieSO fryingRecipieSO in fryingRecipieSOArray)
        {
            if (fryingRecipieSO.input == inputkitchenObjectSO)
            {
                return fryingRecipieSO;
            }
        }

        return null;
    }

    private BurningRecipieSO GetBurningRecipieSO(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (BurningRecipieSO burningRecipieSO in burningRecipieSOArray)
        {
            if (burningRecipieSO.input == inputkitchenObjectSO)
            {
                return burningRecipieSO;
            }
        }

        return null;
    }

    /*
     * Returns the output KitchenObjectSO from the CuttingRecipieSO given the input KitchenObjectSO
     */
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        return GetFryingRecipieSO(inputkitchenObjectSO) != null ? GetFryingRecipieSO(inputkitchenObjectSO).output : null;
    }

    private bool HasRecipieForInput(KitchenObjectSO inputkitchenObjectSO)
    {
        return GetFryingRecipieSO(inputkitchenObjectSO) != null;
    }

    private void SetState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = newState });
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}
