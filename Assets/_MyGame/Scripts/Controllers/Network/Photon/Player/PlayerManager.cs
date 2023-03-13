using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] public GameObject PlayerUiPrefab;
    public static GameObject LocalPlayerInstance;
    private CameraFollow _camera;
    private PlayerInfoPanelUI _panelUI;
    private float _health;
    private float _score;
    private bool _isAlive = true;

    public PlayerInfoPanelUI PanelUI => _panelUI;
    public float Health
    {
        get => _health;
        set => _health = value;
    }

    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if (_isAlive == false)
                return;

            _isAlive = value;
            
            if(photonView.IsMine) 
                //photonView.RPC("DestroySelf", RpcTarget.AllBuffered, gameObject.GetComponent<PhotonView>().ViewID);
                PhotonNetwork.Destroy(photonView);
        }
    }

    public float Score
    {
        get => _score;
        set => _score = value;
    }

    // [PunRPC] 
    // public void DestroySelf(int viewId) 
    // { 
    //     PhotonNetwork.Destroy(PhotonView.Find(viewId).gameObject); 
    // }  
    
    #region Unity Callbacks

    private void Awake()
    {
        if (photonView.IsMine)
            LocalPlayerInstance = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.gameObject.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        this.gameObject.GetComponent<PlayerScore>().SetTarget(this);

        if (PlayerUiPrefab != null)
        {
            GameObject uiGo =  Instantiate(PlayerUiPrefab);
            uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
            
            _panelUI = uiGo.GetComponent<PlayerInfoPanelUI>();
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

        var rect = _panelUI.gameObject.GetComponent<RectTransform>();
        var pos = rect.anchoredPosition;

        if (PhotonNetwork.IsMasterClient && !photonView.IsMine)
        {
            rect.anchoredPosition = new Vector2(800, pos.y);
        }

        if (!PhotonNetwork.IsMasterClient && photonView.IsMine)
        {
            rect.anchoredPosition = new Vector2(800, pos.y);
        }

        _camera = this.GetComponent<CameraFollow>();
        if(_camera != null)
            if (photonView.IsMine)
                _camera.OnStartFollowing();
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraFollow Component on playerPrefab.", this);
            }
    }

    #endregion

    // private void InitHP()
    // {
    //
    //         var character = LocalPlayerInstance.GetComponent<CharacterController2D>();
    //         character.OnChangeHP += ChangeHP;
    //         Health = character.Health;
    // }
    //
    // private void InitScore()
    // {
    //     var scoreCharacter = LocalPlayerInstance.GetComponent<PlayerScore>();
    //     scoreCharacter.OnChangeScore += ChangeScore;
    // }

    // private void ChangeScore(int score)
    // {
    //     Score = score;
    // }
    //
    // private void ChangeHP(float value)
    // {
    //     Health = value;
    // }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
            stream.SendNext(Score);
        }

        else
        {
            Health = (float)stream.ReceiveNext();
            Score = (float)stream.ReceiveNext();
        }
    }

    public void CalledOnLevelWasLoaded()
    {
        GameObject _uiGo = Instantiate(this.PlayerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            if(PhotonRoomManager.Instance == null)
                return;
            
            FindObjectOfType<PhotonRoomManager>().RespawnPlayer();
        }

    }
}
