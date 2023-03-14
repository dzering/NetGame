using System;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabAccountManager : MonoBehaviour
{
    public event Action<string> OnGetAccountInfo;
    private string _accountInfo = string.Empty;

    public string AccountInfo
    {
        get => _accountInfo;
        set => _accountInfo = value;
    }

    public void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccountSuccess, OnFailure);

    }
    private void GetUserData(string myPlayFabID)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabID,
            Keys = null},
            result => {
                Debug.Log("Got user data:");
                if (result.Data == null || !result.Data.ContainsKey("Score")) Debug.Log("No Score");
                else Debug.Log("Score: "+result.Data["Score"].Value);
            }, (error) => {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
            });
    }
    

    private void OnGetAccountSuccess(GetAccountInfoResult result)
    {
        // var info = result.AccountInfo.Username;
        // var newString = new StringBuilder($"Name: {info}\n");
        // newString.AppendLine($"ID: {info}");
        //
        // _accountInfo = newString.ToString();
        // OnGetAccountInfo?.Invoke("");
    }

    private void OnFailure(PlayFabError obj)
    {
        Debug.LogError("OnFailuer PlayFab accaunt");
    }
}
