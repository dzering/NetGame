using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class GameController : BaseController
{
    private PhotonGameManager _photonGameManager;

    public GameController(Context context)
    {
       Init(context);
    }

    private void Init(Context context)
    {
        var photonGameManagerGameObject = Object.Instantiate(context.PhotonGameManager);
        AddGameObject(photonGameManagerGameObject);
        _photonGameManager = photonGameManagerGameObject.GetComponent<PhotonGameManager>();
    }
    
}
