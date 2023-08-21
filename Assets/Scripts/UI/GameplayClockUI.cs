using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Awake()
    {
        timerImage.color = Color.green;
    }

    private void Update()
    {
        timerImage.fillAmount = GameHandler.Instance.GetGamePlayingTimerNormalized();
        SetTimerColor();
    }

    private void SetTimerColor()
    {
        if (GameHandler.Instance.GetGamePlayingTimerNormalized() < 0.5)
        {
            timerImage.color = Color.green;
        }
        else if (GameHandler.Instance.GetGamePlayingTimerNormalized() < 0.75)
        {
            timerImage.color = Color.yellow;
        }
        else
        {
            timerImage.color = Color.red;
        }
    }
}
