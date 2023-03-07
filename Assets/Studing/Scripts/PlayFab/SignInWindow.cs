using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class SignInWindow : AccountWindowBase
{
    [SerializeField] private Button _signInButton;

    private void Start()
    {
        SubscriptionElementsUI();
    }

    protected override void SubscriptionElementsUI()
    {
        base.SubscriptionElementsUI();

        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        OnLoadingStarted?.Invoke();
        Debug.Log("SignIn is started");
        var request = new LoginWithPlayFabRequest()
        {
            Username = _userName,
            Password = _password
        };

        PlayFabClientAPI.LoginWithPlayFab(
            request,
            result =>
            {
                Debug.Log($"Success: {_userName}");
                EnterInGameScene();
            }, error =>
            {
                OnLoadingFinished(false);
                Debug.LogError($"Fail: {error}");
            }
            );
    }
}
