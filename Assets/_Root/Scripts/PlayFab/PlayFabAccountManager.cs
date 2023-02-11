using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;


public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleLable;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetInfo, OnError);
    }

    private void OnGetInfo(GetAccountInfoResult obj)
    {
        _titleLable.text = $"Player id: {obj.AccountInfo.PlayFabId}";
    }

    private void OnError(PlayFabError obj)
    {
        var errorMessage = obj.GenerateErrorReport();
        Debug.Log(errorMessage);
    }
}
