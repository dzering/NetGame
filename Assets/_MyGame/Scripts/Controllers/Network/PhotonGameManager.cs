using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    #region Fields

    public event Action<GameObject> OnInstantiatePlayer;
    
    [SerializeField] private GameObject _playerPrefab;
    public static PhotonGameManager Instance; 

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            //SceneManager.LoadScene("GameLauncher");
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("PhotonNetwork.IsConnected");
        }

        if (_playerPrefab == null)
        {
            Debug.Log("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'");
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
               PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
               OnInstantiatePlayer?.Invoke(_playerPrefab);
            }
        }
    }

    #endregion

    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log("OnPlayerEnteredRoom: " + other.NickName);
        
        if(PhotonNetwork.IsMasterClient)
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    #endregion
    
}
