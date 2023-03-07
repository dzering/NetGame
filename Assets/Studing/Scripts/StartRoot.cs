using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFabClient;


public class StartRoot : MonoBehaviour
{
    [SerializeField] private PlayFabLauncherAnonymus _playFab;
    [SerializeField] private LoginPanelUI _playFabPanelUI;

    [SerializeField] private PhotonLauncher _photon;
    [SerializeField] private LoginPanelUI _photonPanelUI;

    private LoginPanel _playFabPanel;
    private LoginPanel _photonPanel;

    private void Start()
    {
        _playFabPanel = new LoginPanel(_playFabPanelUI, "PlayFab");
        _photonPanel = new LoginPanel(_photonPanelUI, "Photon");

        _playFabPanel.onConnect += _playFab.Login;

        _photonPanel.onConnect += _photon.Connect;
        _photonPanel.onDisconnect+= _photon.Disconnect;

        _playFab.OnLogin += _playFabPanel.UpdateStatus;
        _photon.onConnection += _photonPanel.UpdateStatus;
    }

    private void OnDestroy()
    {
        _playFabPanel.onConnect -= _playFab.Login;
        _photonPanel.onConnect -= _photon.Connect;
        _photonPanel.onDisconnect -= _photon.Disconnect;
        _photon.onConnection -= _photonPanel.UpdateStatus;
    }
}
