using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

public class MainMenu
{
    private readonly MainMenuUI _mainMenuUI;
    private readonly GameObject[] _gameObjects;

    /// <param name="gameObjects">Objects on the scene in Main menu that should be deactivate after close menu</param>
    public MainMenu(MainMenuUI mainMenuUI, params GameObject[] gameObjects)
    {
        _mainMenuUI = mainMenuUI;
        _mainMenuUI.Init(OpenSettingMenu, StartGame, CloseApplication);
        _gameObjects = gameObjects;

        GameStateManager.Instance.OnSettingMenu += TurnOffMainMenu;
        GameStateManager.Instance.OnMainMenu += TurnOnMainMenu;
    }

    public void TurnOffMainMenu()
    {
        foreach (var item in _gameObjects)
        {
            item.SetActive(false);
        }
        _mainMenuUI.gameObject.SetActive(false);
    }

    public void TurnOnMainMenu()
    {
        foreach (var item in _gameObjects)
        {
            item.SetActive(true);
        }
        _mainMenuUI.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        GameStateManager.Instance.CurrentState = GameState.StartGame;
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void OpenSettingMenu()
    {
        GameStateManager.Instance.CurrentState = GameState.SettingMenu;
    }

    public void UpdateInfo(string text)
    {
        _mainMenuUI.UpdateInfoText(text);
    }
}
