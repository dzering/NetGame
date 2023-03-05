using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    private GameModel _gameModel;
    [SerializeField] private GameObject enemyForMainMenu;
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private SettingMenuUI _settingMenuUI;
    [SerializeField] private MusicManager _musicManager;
    
    private MainMenu _mainMenu;
    private SettingMenu _settingMenu;
    private GameStateManager GameStateManager;
    private GameSetting _gameSetting;

    private SaveDataRepository _saveDataRepository;
    public void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _gameSetting = new GameSetting();
        // GameStateManager = new GameStateManager(_gameSetting);
        // GameStateManager.CurrentState = GameState.None;
        
        //_settingMenu = new SettingMenu(_settingMenuUI, _gameSetting.SoundData);
        //_mainMenu = new MainMenu(_mainMenuUI, enemyForMainMenu);

        _saveDataRepository = new SaveDataRepository();
        
        _saveDataRepository.Load(_gameSetting.SoundData);
        _settingMenu.OnCloseSettingMenu += _saveDataRepository.Save;
        _musicManager.Play();

    }
}
