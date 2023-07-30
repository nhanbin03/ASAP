using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;


    void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.Home);
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.Home:
                break;
            case GameState.Gameplay:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    
}

public enum GameState {
    Home,
    Gameplay,
    Victory,
    Lose
}