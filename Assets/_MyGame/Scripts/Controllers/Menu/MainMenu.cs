using UnityEngine;
using Object = UnityEngine.Object;

public class MainMenu : BaseController
{
    private MainMenuUI _mainMenuUI;
    private GameObject[] _gameObjects;
    private readonly Context _context;

    /// <param name="gameObjects">Objects on the scene in Main menu that should be deactivate after close menu</param>
    /// <param name="mainMenuUI">Prefab of UI. Should has a component MainMenuUI.cs</param>
    public MainMenu(Context context, GameObject mainMenuUI, params GameObject[] gameObjects)
    {
        _context = context;
        Init(context.PlaceForUI, mainMenuUI, gameObjects);
    }

    private void Init(Transform placeForUI, GameObject prefabMainMenuUI, GameObject[] gameObjects)
    {
        var go = Object.Instantiate(prefabMainMenuUI, placeForUI);
        go.SetActive(true);
        
        if(!go.TryGetComponent(out _mainMenuUI))
            return;

        _mainMenuUI.Init(OpenSettingMenu, StartGame, CloseApplication);
        _gameObjects = gameObjects;
        
        AddGameObject(go);
        foreach (var gameObject in gameObjects)
        {
            AddGameObject(gameObject);
        }
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
    }

    private void CloseApplication()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void OpenSettingMenu()
    {
        Debug.Log("Setting Menu");
        _context.GameModel.CurrentState = GameState.SettingMenu;
    }

    private void UpdateInfo(string text)
    {
        _mainMenuUI.UpdateInfoText(text);
    }
}
