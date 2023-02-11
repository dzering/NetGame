using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLauncher : MonoBehaviour
{
    private const string AUTH_GUID_KEY = nameof(AUTH_GUID_KEY);
    public event Action<bool> OnLogin;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = "AB304";
    }

    public void Login()
    {
        var needCreate = PlayerPrefs.HasKey(AUTH_GUID_KEY);
        var id = PlayerPrefs.GetString(AUTH_GUID_KEY, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreate,
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                PlayerPrefs.SetString(AUTH_GUID_KEY, id);
                OnLoginSuccess(result);
            }, OnLoginError);
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
