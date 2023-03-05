using System;

public sealed class MainController
{
    private readonly Context _context;
    private MainMenu _mainMenuController;
    private SettingMenu _settingMenu;
    public MainController(Context context)
    {
        _context = context;
        _context.GameModel.OnChangeGameState += ChangeController;
    }

    private void ChangeController(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.MainMenu:
                _mainMenuController = new MainMenu(_context, _context.UISO.MainMenuGo);
                break;
            case GameState.SettingMenu:
                _settingMenu = new SettingMenu(_context, new SoundDataSetting());
                break;
            case GameState.StartGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

}