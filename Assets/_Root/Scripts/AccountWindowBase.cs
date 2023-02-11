using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountWindowBase : MonoBehaviour
{
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _passwordField;

    protected string _userName;
    protected string _password;

    protected virtual void SubscriptionElementsUI()
    {
        _userNameField.onValueChanged.AddListener(UpdateUserName);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }

    private void UpdatePassword(string password)
    {
        _password = password;
    }

    private void UpdateUserName(string userName)
    {
        _userName = userName;
    }
}
