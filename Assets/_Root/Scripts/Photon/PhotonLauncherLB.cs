using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using System;
using System.Text;

public class PhotonLauncherLB : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    public event Action<List<RoomInfo>> OnUpdateRooms;

    private const string MAP_PROP_KEY = "C0";
    private const string GOLD_PROP_KEY = "C1";
    private readonly TypedLobby _sqlTypedLobby = new TypedLobby("customSQLLobby", LobbyType.SqlLobby);
    private readonly TypedLobby _defaultLobby = new TypedLobby("default", LobbyType.Default);

    [SerializeField] private ServerSettings _serverSettings;
    [SerializeField] private TMP_Text _stateUiText;

    [SerializeField] private TMP_Text _placeForInfo;

    private LoadBalancingClient _lbc;
    private readonly List<RoomInfo> _cashedRoomList = new List<RoomInfo>();

    #region UnityMethods

    private void Start()
    {
        _lbc = new LoadBalancingClient();
        _lbc.AddCallbackTarget(this);

        _lbc.ConnectUsingSettings(_serverSettings.AppSettings);

        var state = _lbc.State.ToString();
        _stateUiText.text = state;
    }

    private void OnDestroy()
    {
        _lbc.RemoveCallbackTarget(this);
    }

    private void Update()
    {
        if(_lbc == null)
            return;
        
        _lbc.Service();
    }

    #endregion

    #region CustomMethods

    public void CloseCurrentRoom()
    {
        _lbc.CurrentRoom.IsOpen = false;
        UpdateStateText($"{_lbc.CurrentRoom.Name} is closed");
        Debug.Log("Room has been closed");
    }
    private void UpdateStateText(string text)
    {
        _stateUiText.text = text;
    }
    private void UpdateRoomsList(IEnumerable<RoomInfo> roomList)
    {
        foreach (var room in roomList)
        {
            var cashedRoom = room;
            if (cashedRoom.RemovedFromList)
            {
                _cashedRoomList.Remove(cashedRoom);
            }
            else
            {
                _cashedRoomList.Add(cashedRoom);
            }
        }
        OnUpdateRooms?.Invoke(_cashedRoomList);
    }

    public void JoinRoomOrCreateAndJoin()
    {
        _lbc.OpJoinRandomOrCreateRoom(null, null);
    }

    public void JoinCreatePrivateRoom(string namePrivateRoom)
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = false,  
            MaxPlayers = 4,
        };
        
        EnterRoomParams roomParams = new EnterRoomParams()
        {
            RoomName = namePrivateRoom,
            RoomOptions = roomOptions,
        };
        _lbc.OpJoinOrCreateRoom(roomParams);
    }

    public void JoinRoom(string roomName)
    {
        var roomParam = new EnterRoomParams()
        {
            RoomName = roomName,
        };
        _lbc.OpJoinRoom(roomParam);
    }

    private void ShowRoomParams(TMP_Text placeForInfo)
    {
        var info = new StringBuilder();
        
        var maxPlayer = _lbc.CurrentRoom.MaxPlayers.ToString();
        info.Append($"Max player: {maxPlayer}\n");

        var playerCount = _lbc.CurrentRoom.PlayerCount.ToString();
        info.Append($"Player count: {playerCount}\n");

        int playerNumber = 0;
        foreach (var player in _lbc.CurrentRoom.Players)
        {
            playerNumber++;
            info.Append($"Player {playerNumber}: {player.Value.UserId}\n");
        }

        _placeForInfo.text = info.ToString();
    }

    #endregion

    #region IMatchmakingCallbacks

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    public void OnCreatedRoom()
    {
        Debug.Log("Room is created");
        UpdateStateText("Room is created");
    }

    public void OnJoinedRoom()
    {
        var message = _lbc.CurrentRoom.Name;
        Debug.Log($"Joined to the room{message}");
        UpdateStateText($"Joined to the room {message}");
        ShowRoomParams(_placeForInfo);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        UpdateStateText("OnJoinRoomFailed");
        Debug.Log("OnJoinRoomFailed");
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

    }

    public void OnLeftRoom()
    {
        
    }

    #endregion

    #region ConnectionCallbacks

    void IConnectionCallbacks.OnConnected()
    {
        
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
        var message = "Connected to master";
        Debug.Log("Connected to master");
        UpdateStateText(message);
    }

    void  IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
    {
        
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        
    }

    public void JoinLobby()
    {
        _lbc.OpJoinLobby(_defaultLobby);
    }

    #endregion

    #region ILobbyCallbacks

    void ILobbyCallbacks.OnJoinedLobby()
    {
        _cashedRoomList.Clear();
        Debug.Log("OnJoinedLobby");
        // var sqlLobbyFilter = $"{MAP_PROP_KEY} = map_forest_winter_01 AND {GOLD_PROP_KEY} BETWEEN 300 AND 500";
        // var opJoinRandomRoomParams = new OpJoinRandomRoomParams
        // {
        //     SqlLobbyFilter = sqlLobbyFilter,
        // };
        // _lbc.OpJoinRandomRoom(opJoinRandomRoomParams);
        UpdateStateText("OnJoinedLobby");
    }

    public void OnLeftLobby()
    {
        _cashedRoomList.Clear();
    }

    void ILobbyCallbacks.OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomsList(roomList);
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        
    }

    #endregion
}
