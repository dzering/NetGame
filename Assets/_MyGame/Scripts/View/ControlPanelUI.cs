using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControlPanelUI : MonoBehaviour
{
    #region Fields

    private const string PLAYER_NAME_PREF_KEY = "PlayerName";

    [SerializeField] private TMP_Text _nameLable;
    [SerializeField] private Button _connectionButton;
    [SerializeField] private TMP_InputField _nameInputField;

    #endregion
    
    #region MonoBehaviour Callback

    private void OnDestroy()
    {
        _connectionButton.onClick.RemoveAllListeners();
        _nameInputField.onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        Init();
    }

    #endregion
    
    #region Methods

    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.Log("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(PLAYER_NAME_PREF_KEY, value);


    }
    private void Init()
    {
        _nameInputField.onValueChanged.AddListener(SetPlayerName);
        string defaultName = string.Empty;
        if (_nameInputField != null)
        {
            if (PlayerPrefs.HasKey(PLAYER_NAME_PREF_KEY))
            {
                defaultName = PlayerPrefs.GetString(PLAYER_NAME_PREF_KEY);
                _nameInputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void OnConnectionButtonSubscribe(UnityAction connection)
    {
        _connectionButton.onClick.AddListener(connection);
    }

    public void OnConnectionButtonUnsubscribe(UnityAction connection)
    {
        _connectionButton.onClick.RemoveListener(connection);
    }

    #endregion
}