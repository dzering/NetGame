using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public sealed class GameController : BaseController
{
    private PlayerInfoPanelController _playerInfoPanelController;

    public GameController(Context context, Transform placeForUI)
    {
       Init(context, placeForUI);
    }

    private void Init(Context context, Transform placeForUI)
    {
        _playerInfoPanelController = new PlayerInfoPanelController(context, placeForUI);
        AddDisposable(_playerInfoPanelController);
        
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }
}
