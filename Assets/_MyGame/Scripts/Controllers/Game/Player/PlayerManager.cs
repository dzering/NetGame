using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject LocalPlayerInstance;
    private CameraFollow _camera;

    private void Start()
    {
        _camera = this.GetComponent<CameraFollow>();
        if(_camera != null)
            if (photonView.IsMine)
                _camera.OnStartFollowing();
      //  DontDestroyOnLoad(gameObject);
    }
    
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
        }

        if (stream.IsWriting)
        {
            
        }
    }
}
