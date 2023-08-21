using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;
        Hide();
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulReceipiesDelivered().ToString();
            Show();
        }
        else
        {
            Hide();
        }
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
