using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] public GameObject PlayerUiPrefab;
    public static GameObject LocalPlayerInstance;
    private CameraFollow _camera;
    private PlayerInfoPanelUI _panelUI;

    public PlayerInfoPanelUI PanelUI => _panelUI;
    public float Health { get; set; }

    #region Unity Callbacks
    
    private void Awake()
    {
        if (photonView.IsMine)
            LocalPlayerInstance = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        InitHP();

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
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
    }

    #endregion

    private void InitHP()
    {
        var character = gameObject.GetComponent<CharacterController2D>();
        character.OnChangeHP += ChangeHP;
        Health = character.Health;
    }

    private void ChangeHP(float value)
    {
        Health = value;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Health);
            Debug.Log($"{photonView.Owner.NickName} + {photonView.GetHashCode()}: send message");
        }

        else
        {
            Health = (float)stream.ReceiveNext();
            Debug.Log($"{photonView.Owner.NickName} + {photonView.GetHashCode()}: recive message");
        }
    }

    public void CalledOnLevelWasLoaded()
    {
        GameObject _uiGo = Instantiate(this.PlayerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

}
