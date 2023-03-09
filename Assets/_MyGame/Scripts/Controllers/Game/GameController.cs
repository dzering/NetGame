using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public sealed class GameController : BaseController
{
    private PlayerInfoPanelController _playerInfoPanelController;

    public GameController(Context context)
    {
       Init(context);
    }

    private void Init(Context context)
    {
        _playerInfoPanelController = new PlayerInfoPanelController(context);
        AddDisposable(_playerInfoPanelController);
        
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }
}
