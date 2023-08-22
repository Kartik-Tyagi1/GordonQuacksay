using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameLoader
{
    public enum GameScene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static GameScene targetScene;

    public static void Load(GameScene targetScene)
    {
        GameLoader.targetScene = targetScene;

        SceneManager.LoadScene(GameScene.LoadingScene.ToString());
    }

    public static void GameLoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
