using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class ConnectAndJoinRandomLB : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    private const string MAP_PROP_KEY = "C0";
    private const string GOLD_PROP_KEY = "C1";
    private readonly TypedLobby _sqlTypedLobby = new TypedLobby("customSQLLobby", LobbyType.SqlLobby);

    [SerializeField] private ServerSettings _serverSettings;
    [SerializeField] private TMP_Text _stateUiText;

    [SerializeField] private TMP_Text _maxPlayer;

    private LoadBalancingClient _lbc;

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

    public void OnConnected()
    {
        
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        var roomOptions = new RoomOptions()
        {
            MaxPlayers = 4,
            PublishUserId = true,
            CustomRoomPropertiesForLobby = new []{MAP_PROP_KEY, GOLD_PROP_KEY},
            CustomRoomProperties = new Hashtable()
            {
                {GOLD_PROP_KEY, 400},
                {MAP_PROP_KEY, "map_forest_winter_01"}, 
            }
        };

        var roomParams = new EnterRoomParams
        {
            RoomName = "New room",
            RoomOptions = roomOptions,
            Lobby = _sqlTypedLobby,
            PlayerProperties = null,
        };

        _lbc.OpCreateRoom(roomParams);
    }

    public void OnDisconnected(DisconnectCause cause)
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

    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        
    }

    public void OnCreatedRoom()
    {
        Debug.Log("Room is created");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Joined to the room");
        ShowRoomParams();
    }

    private void ShowRoomParams()
    {
        var maxPlayer = _lbc.CurrentRoom.MaxPlayers.ToString();
        var text = _maxPlayer.text;
        _maxPlayer.text = text + maxPlayer;

        var info = _lbc.CurrentRoom.Players.First().Value.UserId;
        Debug.Log(info);
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

    }

    public void OnLeftRoom()
    {
        
    }

    public void OnJoinedLobby()
    {
        var sqlLobbyFilter = $"{MAP_PROP_KEY} = map_forest_winter_01 AND {GOLD_PROP_KEY} BETWEEN 300 AND 500";
        var opJoinRandomRoomParams = new OpJoinRandomRoomParams
        {
            SqlLobbyFilter = sqlLobbyFilter,
        };
        _lbc.OpJoinRandomRoom(opJoinRandomRoomParams);
    }

    public void OnLeftLobby()
    {
        
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        
    }
}
