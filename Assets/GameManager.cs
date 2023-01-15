using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state; 

    public static event Action<GameState> onGameStateChange;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState) {
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Lose:
                break;
            case GameState.Win:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChange(newState);
    }
}

public enum GameState {
    PlayerTurn,
    EnemyTurn,
    Win,
    Lose
    
}
