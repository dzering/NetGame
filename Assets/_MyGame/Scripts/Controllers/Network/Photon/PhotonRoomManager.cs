using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    #region Fields
    
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _gameOver;
    
    public static PhotonRoomManager Instance;
    public static float Score;

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

        RespawnPlayer();
        // else
        // {
        //     if (PlayerManager.LocalPlayerInstance == null)
        //     { 
        //       RespawnPlayer();
        //     }
        // }
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     var enemy = PhotonNetwork.InstantiateRoomObject(_enemyPrefab.name, new Vector3(2f, 5f, 0f), Quaternion.identity, 0);    
        // }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(_spawner.name, Vector3.zero, Quaternion.identity);

            var gameOverPosition = GameObject.Find("GameOverPosition").transform.position;
            PhotonNetwork.InstantiateRoomObject(_gameOver.name, gameOverPosition, Quaternion.identity);
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
    


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCourutine());
    }
    private IEnumerator RespawnCourutine()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(30f,-5f,0f), Quaternion.identity, 0);
        // OnInstantiatePlayer?.Invoke(PlayerManager.LocalPlayerInstance);  
    }
}
