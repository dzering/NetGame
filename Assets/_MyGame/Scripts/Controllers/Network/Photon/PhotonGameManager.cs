using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    #region Fields
    
    [SerializeField] private GameObject _playerPrefab;
    [FormerlySerializedAs("_enemy")] [SerializeField] private GameObject _enemyPrefab;
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
               var go = PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
               OnInstantiatePlayer?.Invoke(go);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            var enemy = PhotonNetwork.Instantiate(_enemyPrefab.name, new Vector3(2f, 5f, 0f), Quaternion.identity, 0);    
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

    #endregion

    public event Action<GameObject> OnInstantiatePlayer;
}
