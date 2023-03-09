using Photon.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

public class MainMenu : BaseController
{
    private MainMenuUI _mainMenuUI;
    private GameObject[] _gameObjects;
    private readonly Context _context;

    /// <param name="gameObjects">Objects on the scene in Main menu that should be deactivate after close menu</param>
    /// <param name="mainMenuUI">Prefab of UI. Should has a component MainMenuUI.cs</param>
    public MainMenu(Context context, GameObject mainMenuUI, Transform placeForUI,  params GameObject[] gameObjects)
    {
        _context = context;
        _gameObjects = gameObjects;
        Init(placeForUI, mainMenuUI, gameObjects);
    }

    private void Init(Transform placeForUI, GameObject prefabMainMenuUI, GameObject[] gameObjects)
    {
        var go = Object.Instantiate(prefabMainMenuUI, placeForUI);
        go.SetActive(true);
        
        if(!go.TryGetComponent(out _mainMenuUI))
            return;

        _mainMenuUI.Init(OpenSettingMenu, StartGame, CloseApplication, DoSomeFun, SetPlayerName);
        _gameObjects = gameObjects;
        
        AddGameObject(go);
        foreach (var gameObject in gameObjects)
        {
            go = Object.Instantiate(gameObject, placeForUI.transform);
            AddGameObject(gameObject);
        }

        UpdateWishesText(_context.WishConfig.GetRandomText());
        _context.PlayFabAccount.OnGetAccountInfo += UpdateInfo;

        if(_context.PlayFabAccount.AccountInfo == null)
            return;
        UpdateInfo(_context.PlayFabAccount.AccountInfo);
    }

    private void SetPlayerName(string value)
    {
        throw new System.NotImplementedException();
    }

    private void DoSomeFun()
    {
        throw new System.NotImplementedException();
    }

    private void StartGame()
    {
        _context.GameModel.CurrentState = GameState.StartGame;
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
        _mainMenuUI.UpdatePlayerName(PhotonNetwork.LocalPlayer.NickName);
    }

    private void UpdateWishesText(string wishText)
    {
        _mainMenuUI.UpdateTextFunButton(wishText);
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        _context.PlayFabAccount.OnGetAccountInfo -= UpdateInfo;
    }
}
