using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab.ClientModels;
using UnityEngine.Events;

public class GameStateManager
{
    private GameState _currentState = GameState.None;
    public GameSetting GameSetting;
        
    public event Action OnMainMenu;
    public event Action OnSettingMenu;
    public event Action OnStartGame;
    
    public static GameStateManager Instance;

    public GameStateManager(GameSetting gameSetting)
    {
        if(Instance != null)
            return;

        Instance = this;
        GameSetting = gameSetting;
    }

    public GameState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            NotifyAllStateChanged(value);
        }
    }

    public void UnsubscribeAll()
    {
        OnMainMenu = null;
        OnSettingMenu = null;
        OnStartGame = null;
    }
    private void NotifyAllStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.None:
                break;
            case GameState.MainMenu:
                OnMainMenu?.Invoke();
                break;
            case GameState.SettingMenu:
                OnSettingMenu?.Invoke();
                break;
            case GameState.StartGame:
                OnStartGame?.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
        }
    }
}
