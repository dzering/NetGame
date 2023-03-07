using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PhotonLauncherLB _photonLauncherLB;
    [SerializeField] private Button _connectLobbyButton;
    [SerializeField] private Button _joinGameButton;
    [SerializeField] private Button _quitApplication;
    [SerializeField] private Button _closeCurrentRoomButton;
    [SerializeField] private Button _joinOrCreatePrivateRoom;
    [SerializeField] private TMP_InputField _inputNameRoom;

    [SerializeField] private ContentUI _lobbyScrollView;

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        _connectLobbyButton.onClick.RemoveAllListeners();
        _photonLauncherLB.OnUpdateRooms -= UpdateRooms;
        _joinGameButton.onClick.RemoveAllListeners();
        _quitApplication.onClick.RemoveAllListeners();
        _closeCurrentRoomButton.onClick.RemoveAllListeners();
        _joinOrCreatePrivateRoom.onClick.RemoveAllListeners();
    }

    private void Init()
    {
        _connectLobbyButton.onClick.AddListener(_photonLauncherLB.JoinLobby);
        _photonLauncherLB.OnUpdateRooms += UpdateRooms;
        _joinGameButton.onClick.AddListener(_photonLauncherLB.JoinRoomOrCreateAndJoin);
        _quitApplication.onClick.AddListener(QuitApplication);
        
        _lobbyScrollView.Init(_photonLauncherLB);
        _closeCurrentRoomButton.onClick.AddListener(CloseCurrentRoom);
        
        _joinOrCreatePrivateRoom.onClick.AddListener(JoinOrCreatePrivateRoom);
    }

    private void JoinOrCreatePrivateRoom()
    {
        _photonLauncherLB.JoinCreatePrivateRoom(_inputNameRoom.text);
    }

    private void UpdateRooms(List<RoomInfo> roomsList)
    {
        _lobbyScrollView.UpdateContent(roomsList);
    }

    private void CloseCurrentRoom()
    {
        _photonLauncherLB.CloseCurrentRoom();
        var text = _closeCurrentRoomButton.GetComponentInChildren<TMP_Text>();
        text.text = "Room is closed";
    }

    private void QuitApplication()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}