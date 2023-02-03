using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLogin : MonoBehaviour
{
    public event Action<bool> OnLogin;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = "AB304";

    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "Player01",
            CreateAccount = true,
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        OnLogin?.Invoke(true);
        Debug.Log("Success.");
    }

    private void OnLoginError(PlayFabError obj)
    {
        var message = obj.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {message}");
        OnLogin?.Invoke(false);
    }
}
