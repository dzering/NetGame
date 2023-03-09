using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public sealed class MainController : BaseController
{
    #region Fields

    private readonly Context _context;
    private MainMenu _mainMenuController;
    private SettingMenu _settingMenu;
    private readonly Transform _placeForUI;

    private PhotonLauncher _photonLauncher;
    private GameController _gameController;

    #endregion

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
                if(_photonLauncher.IsConnected)
                     return;
                _photonLauncher.Connect();
                _context.GameModel.CurrentState = GameState.LoadingGame;
                break;
            
            case GameState.LoadingGame:
                _context.GameModel.CurrentState = GameState.Game;
                break;
            
            case GameState.Game:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public MainController(Context context, Transform placeForUI)
    {
        _context = context;
        _placeForUI = placeForUI;

        var photon = Object.Instantiate(_context.PhotonLauncher);
        _photonLauncher = photon.GetComponent<PhotonLauncher>();
        AddGameObject(photon.gameObject);

        _context.GameModel.OnChangeGameState += ChangeController;
        SceneManager.activeSceneChanged += ChangeActiveScene;
    }

    private void ChangeActiveScene(Scene current, Scene next)
    {
        _gameController = new GameController(_context);
        AddDisposable(_gameController);
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