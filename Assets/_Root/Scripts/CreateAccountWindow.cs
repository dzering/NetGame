using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFabClient;
using PlayFab.ClientModels;

public class CreateAccountWindow : AccountWindowBase
{
    [SerializeField] private InputField _emailField;
    [SerializeField] private Button _createAccount;
    
    private string _email;

    protected override void SubscriptionElementsUI()
    {
        base.SubscriptionElementsUI();

        _emailField.onValueChanged.AddListener(UpdateEmail);
        _createAccount.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        var request = new RegisterPlayFabUserRequest {
            Email = _email,
            Password = _password,
            Username = _userName
        };

        PlayFabClientAPI.RegisterPlayFabUser(
            request,
            result =>
            {
                Debug.Log($"Success: {_userName}");
            },
                error =>
                {
                    Debug.Log($"Fail: {error.ErrorMessage}");
                });
    }

    private void UpdateEmail(string email)
    {
        _email= email;
    }
}
