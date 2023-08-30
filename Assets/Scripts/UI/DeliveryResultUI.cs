using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image IconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;
    private const string POPUP = "Popup";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecepieSuccess += DeliveryManager_OnRecepieSuccess;
        DeliveryManager.Instance.OnRecepieFailed += DeliveryManager_OnRecepieFailed;

        Hide();
    }

    private void DeliveryManager_OnRecepieFailed(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = failColor;
        IconImage.sprite = failSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecepieSuccess(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        IconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
