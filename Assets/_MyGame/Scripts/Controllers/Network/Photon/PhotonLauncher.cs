using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    #region Fields

    //[SerializeField] private ControlPanelUI _controlPanelUI;
    // [SerializeField] private Text _feedbackText;
    [SerializeField] private byte _maxPlayer = 2;

    private bool _isConnected;
    private readonly string _gameVersion = "1";

    public bool IsConnected => _isConnected;

    #endregion

    #region Monobehaviour Callback

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
       // _controlPanelUI.OnConnectionButtonSubscribe(Connect);
    }

    private void OnDestroy()
    {
       // _controlPanelUI.OnConnectionButtonUnsubscribe(Connect);
    }

    #endregion

    #region Methods

    public void ConnectToServer()
    {
        if(_isConnected)
            return;
        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = _gameVersion;
        _isConnected = true;
        
        Debug.Log("ConnectToServer");
    }
    public void Connect()
    {
        // _feedbackText.text = "";
        _isConnected = true;

        if (PhotonNetwork.IsConnected)
        {
           // LogFeedback("Joining Room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
          //  LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    // private void LogFeedback(string message)
    // {
    //     if(_feedbackText == null)
    //         return;
    //
    //     _feedbackText.text += System.Environment.NewLine + message;
    // }

    #endregion

    #region MonoBehaviourPunCallbacks

    public override void OnConnectedToMaster()
    {
        if (IsConnected)
        {
           // LogFeedback("<Color=Blue>OnConnectedToMaster</Color>: Next -> Try to join random room.");
            Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
       // LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
        PhotonNetwork.CreateRoom(null, 
            new RoomOptions()
            {
                MaxPlayers = _maxPlayer,
            });
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
    }

    public override void OnJoinedRoom()
    {
        string name = PhotonNetwork.CurrentRoom.Name;
       // LogFeedback($"<Color=Green>OnJoinedRoom</Color> with {PhotonNetwork.CurrentRoom.PlayerCount} players");
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running." + name);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("Level_01");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        _isConnected = false;
        Debug.LogError(cause);
    }

    #endregion
}