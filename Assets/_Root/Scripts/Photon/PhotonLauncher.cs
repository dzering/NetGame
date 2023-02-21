using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    public event Action<bool> onConnection;
    private string gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("Photon was disconnected.");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connect to master was complited succesfull");
    }

    public override void OnConnected()
    {
        base.OnConnected();
        onConnection?.Invoke(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        onConnection?.Invoke(false);
    }
}
