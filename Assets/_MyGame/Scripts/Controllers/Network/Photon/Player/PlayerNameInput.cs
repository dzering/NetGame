using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    private const string PLAYER_NAME_PREF_KEY = "PlayerName";

    private void Start()
    {
        string defaultName = String.Empty;
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(PLAYER_NAME_PREF_KEY))
            {
                defaultName = PlayerPrefs.GetString(PLAYER_NAME_PREF_KEY);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;

        _inputField.onEndEdit.AddListener(SetPlayerName);
    }

    public void SetPlayerName(string nickName)
    {
        if (string.IsNullOrEmpty(nickName))
        {
            Debug.LogError("Player name is null or empty");
            return;
        }

        PhotonNetwork.NickName = nickName;
        
        PlayerPrefs.SetString(PLAYER_NAME_PREF_KEY, nickName);
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveAllListeners();
    }
}
