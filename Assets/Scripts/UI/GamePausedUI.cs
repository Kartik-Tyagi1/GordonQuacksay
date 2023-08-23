using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameHandler.Instance.TogglePauseGame();
        });

        optionsMenuButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            GameLoader.Load(GameLoader.GameScene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameHandler.Instance.OnGamePaused += GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        Hide();
    }
    private void GameHandler_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
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
