using System;
using UnityEngine;

public sealed class MainController : BaseController
{
    private readonly Context _context;
    private MainMenu _mainMenuController;
    private SettingMenu _settingMenu;

    private PhotonLauncher _photonLauncher;
    
    public MainController(Context context)
    {
        _context = context;
        _context.GameModel.OnChangeGameState += ChangeController;
        var photon = GameObject.Instantiate(_context.PhotonLauncher);
        _photonLauncher = photon.GetComponent<PhotonLauncher>();
    }

    private void ChangeController(GameState state)
    {
        DisposeChildObject();
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.MainMenu:
                _mainMenuController = new MainMenu(_context, _context.UISO.MainMenuGo);

                break;
            case GameState.SettingMenu:
                _settingMenu = new SettingMenu(_context);

                break;
            case GameState.StartGame:
                _photonLauncher.Connect();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        DisposeChildObject();
        _context.GameModel.OnChangeGameState -= ChangeController;
    }
    
    private void DisposeChildObject()
    {
        _settingMenu?.Dispose();
        _mainMenuController?.Dispose();
    }

}