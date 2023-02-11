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
        var request = new LoginWithPlayFabRequest()
        {
            Username = _userName,
            Password = _password
        };

        PlayFabClientAPI.LoginWithPlayFab(
            request,
            result =>
            {
                EnterInGameScene();
                Debug.Log($"Success: {_userName}");
            }, error =>
            {
                Debug.LogError($"Fail: {error}");
            }
            );
    }
}
