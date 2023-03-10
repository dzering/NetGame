using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class GameController : BaseController
{
    private PlayerInfoPanelController _playerInfoPanelController;
    private PhotonGameManager _photonGameManager;

    public GameController(Context context)
    {
       Init(context);
    }

    private void Init(Context context)
    {
        _playerInfoPanelController = new PlayerInfoPanelController(context);
        AddDisposable(_playerInfoPanelController);
        
        var photonGameManagerGameObject = Object.Instantiate(context.PhotonGameManager);
        AddGameObject(photonGameManagerGameObject);
        _photonGameManager = photonGameManagerGameObject.GetComponent<PhotonGameManager>();

        _photonGameManager.OnInstantiatePlayer += _playerInfoPanelController.SetTarget;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        _photonGameManager.OnInstantiatePlayer -= _playerInfoPanelController.SetTarget;
    }
}
