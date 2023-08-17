using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recepieTemplate;

    private void Awake()
    {
        recepieTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecepieCreated += DeliveryManager_OnRecepieCreated;
        DeliveryManager.Instance.OnRecepieRemoved += DeliveryManager_OnRecepieRemoved;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecepieCreated(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecepieRemoved(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Instead of generate many icons we have to remove all icons on this object first then regenerate them when new icon is added
        foreach (Transform child in container)
        {
            // Do not destroy the original template
            if (child == recepieTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipieSO recipieSO in DeliveryManager.Instance.GetWaitingRecipieSOList())
        {
            Transform recepieTransform = Instantiate(recepieTemplate, container);
            // Only show the new templates we make, not the original we use to make the new ones
            recepieTransform.gameObject.SetActive(true);
            recepieTransform.GetComponent<DeliveryManagerSingleUI>().SetRecepieSO(recipieSO);
        }
    }
}
