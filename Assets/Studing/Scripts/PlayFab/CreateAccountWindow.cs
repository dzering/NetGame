//using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFabClient;
using PlayFab.ClientModels;


public class CreateAccountWindow : AccountWindowBase
{
    [SerializeField] private InputField _emailField;
    [SerializeField] private Button _createAccountButton;
    
    private string _email;

    private void Start()
    {
        SubscriptionElementsUI();
    }

    protected override void SubscriptionElementsUI()
    {
        base.SubscriptionElementsUI();

        _emailField.onValueChanged.AddListener(UpdateEmail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        OnLoadingStarted?.Invoke();
        var request = new RegisterPlayFabUserRequest {
            Email = _email,
            Password = _password,
            Username = _userName
        };

        PlayFabClientAPI.RegisterPlayFabUser(
            request, result =>
            {
                OnLoadingStarted?.Invoke();
                EnterInGameScene();
                Debug.Log($"Success: {_userName}");
            }, error =>
            {
                OnLoadingFinished?.Invoke(false);
                Debug.Log($"Fail: {error.ErrorMessage}");
            });
    }

    private void UpdateEmail(string email)
    {
        _email= email;
    }
}
