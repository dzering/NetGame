using System;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public sealed class MainController : BaseController
{
    #region Fields

    private readonly Context _context;
    private MainMenu _mainMenuController;
    private SettingMenu _settingMenu;
    private GameObject _placeForUI;

    private PhotonLauncher _photonLauncher;
    private GameController _gameController;

    #endregion

    public MainController(Context context)
    {
        _context = context;
        _context.GameModel.OnChangeGameState += ChangeController;
        var photon = GameObject.Instantiate(_context.PhotonLauncher);
        _photonLauncher = photon.GetComponent<PhotonLauncher>();
        
        AddGameObject(photon.gameObject);
        _placeForUI = Object.Instantiate(_context.PlaceForUI);
        SceneManager.activeSceneChanged += ChangeActiveScene;
    }

    #region Private Methods

    private void ChangeController(GameState state)
    {
        DisposeChildObject();
        switch (state)
        {
            case GameState.None:
                break;
            
            case GameState.MainMenu:
                _mainMenuController = new MainMenu(_context, _context.UISO.MainMenuGo, _placeForUI);
                break;
            
            case GameState.SettingMenu:
                _settingMenu = new SettingMenu(_context, _placeForUI);
                break;
            
            case GameState.StartGame:
                
                _context.GameModel.CurrentState = GameState.LoadingGame;

                if(_photonLauncher.IsConnected)
                    return;
                _photonLauncher.Connect();
                break;
            
            case GameState.LoadingGame:
                
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void ChangeActiveScene(Scene current, Scene next)
    {
        _gameController = new GameController(_context);
    }

    private void DisposeChildObject()
    {
        _settingMenu?.Dispose();
        _mainMenuController?.Dispose();
    }

    #endregion 

    protected override void OnDispose()
    {
        base.OnDispose();
        DisposeChildObject();
        _context.GameModel.OnChangeGameState -= ChangeController;
        SceneManager.activeSceneChanged -= ChangeActiveScene;
    }
}