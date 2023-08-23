using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            GameLoader.Load(GameLoader.GameScene.GameScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // Reset TimeScale when quit from pause menu
        Time.timeScale = 1.0f;
    }

    
}
