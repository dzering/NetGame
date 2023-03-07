using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabAccountManager : MonoBehaviour
{
    public event Action<string> OnGetAccountInfo;
    private string _accountInfo;

    public string AccountInfo
    {
        get => _accountInfo;
        set => _accountInfo = value;
    }

    public void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccountSuccess, OnFailure);
    }

    private void OnGetAccountSuccess(GetAccountInfoResult result)
    {
        var info = result.AccountInfo.Username;
        var newString = new StringBuilder($"Name: {info}");
        info = result.AccountInfo.PlayFabId;
        newString.AppendLine($"ID: {info}");

        _accountInfo = newString.ToString();
        OnGetAccountInfo?.Invoke(_accountInfo);
    }

    private void OnFailure(PlayFabError obj)
    {
        Debug.LogError("OnFailuer PlayFab accaunt");
    }
}
