using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using UnityEditor;

public class PlayFabLogin : TransferData
{
    public static PlayFabLogin Instance;
    public LoginResult LoginResult;
    
    private const string AUTH_GUID_KEY = "AUTH_GUID_KEY";
    private string _id;

    private void Awake()
    {
         if(Instance!=null)
             Destroy(this);

         Instance = this;
         DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "AB304";
            
        }
        Login();
    }

    public void Login()
    {
        var isNeedCreate = PlayerPrefs.HasKey(AUTH_GUID_KEY);
        _id = PlayerPrefs.GetString(AUTH_GUID_KEY, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest()
        {
            CustomId = _id,
            CreateAccount = !isNeedCreate
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFail);
        Debug.Log(_id);
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        Debug.Log("You have made successful API call!");
        PlayerPrefs.SetString(AUTH_GUID_KEY, _id);

        GetUserData(obj.PlayFabId);
    }

    private void OnLoginFail(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void SetUserData()
    {
        var newData = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"Health", "100"},
            }
        };
        
        PlayFabClientAPI.UpdateUserData(newData, OnUpdateSuccess, OnUpdateFail);
    }

    private void GetUserData(string playFabID)
    {
        var getUserDataRequest = new GetUserDataRequest
        {
            AuthenticationContext = null,
            IfChangedFromDataVersion = null,
            Keys = null,
            PlayFabId = playFabID,
        };
        PlayFabClientAPI.GetUserData(getUserDataRequest, OnGetDataSuccess, OnGetDataFail);
    }

    public void GetUserHealth()
    {
         GetUserData(LoginResult.PlayFabId);
    }

    private void OnGetDataSuccess(GetUserDataResult obj)
    {
        if (obj == null)
            return;

        if (obj.Data.ContainsKey("Health"))
        {
            Debug.Log($"Health = {obj.Data["Health"].Value}");
            OnTransferHealth?.Invoke(obj.Data["Health"].Value);
        }

    }

    private void OnGetDataFail(PlayFabError obj)
    {
        var message = obj.GenerateErrorReport();
        Debug.LogError(message);
    }

    private void OnUpdateSuccess(UpdateUserDataResult obj)
    {
        Debug.Log("User data updetes is success. ");
    }

    private void OnUpdateFail(PlayFabError error)
    {
        var message = error.GenerateErrorReport();
        Debug.Log(message);
    }
}
