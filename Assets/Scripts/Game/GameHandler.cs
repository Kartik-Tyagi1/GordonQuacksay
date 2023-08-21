using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    private enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private GameState gameState;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimerMax = 30f;
    private float gamePlayingTimer;

    public event EventHandler OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
        gameState = GameState.WaitingToStart;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    SetGameState(GameState.CountdownToStart);
                }
                break;
            case GameState.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    SetGameState(GameState.GamePlaying);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    SetGameState(GameState.GameOver);
                }
                break;
            case GameState.GameOver:
                break;
        }
        Debug.Log(gameState);
    }

    private void SetGameState(GameState state)
    {
        gameState = state;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return gameState == GameState.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return gameState == GameState.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        // Timer is decreasing so we need to get the opposite value 
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
}
}
