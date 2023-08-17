using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI receipieNameText;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecepieSO(RecipieSO recipieSO)
    {
        receipieNameText.text = recipieSO.recipieName;
        foreach (Transform child in iconContainer)
        {
            // Do not destroy the original template
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipieSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            // Only show the new templates we make, not the original we use to make the new ones
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
