using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Initializer : MonoBehaviour
{
    [FormerlySerializedAs("_healthPlayer")] [SerializeField] private TMP_Text _healthPlayerText;
    void Start()
    {
        var playFab = GameObject.Find("PlayFab");
        if (playFab == null)
        {
            Debug.Log("PlayFab == null");
            return;            
        }

        
        var playFabLogin = playFab.GetComponent<PlayFabLogin>();

        playFabLogin.OnTransferHealth += SetHealth;
        playFabLogin.GetUserHealth();
    }

    private void SetHealth(string healthText)
    {
        _healthPlayerText.text = healthText;
    }

    private void OnDestroy()
    {
        if(PlayFabLogin.Instance == null)
            return;
        PlayFabLogin.Instance.OnTransferHealth -= SetHealth;
    }
}
