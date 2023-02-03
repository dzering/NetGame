using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFabClient;

public class Root : MonoBehaviour
{
    [SerializeField] private LoginPanelUI _panelView;
    [SerializeField] private PlayFabLogin _playFabLogin;
    [SerializeField] private PhotonLauncher _photon;

    private LoginPanel _loginPanel;

    private void Start()
    {
        _loginPanel = new LoginPanel(_panelView);

        _loginPanel.onConnect += _playFabLogin.Login;
        _loginPanel.onDisconnect += _photon.Disconnect;

        _playFabLogin.OnLogin += _loginPanel.UpdateStatus;
    }

    private void OnDestroy()
    {
        _loginPanel.onConnect -= _playFabLogin.Login;
        _loginPanel.onConnect -= _photon.Disconnect;
    }
}
