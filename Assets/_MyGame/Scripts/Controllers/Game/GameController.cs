using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameController : BaseController
{
    private PlayerInfoPanelUIController _playerInfoPanelUIController;

    public GameController(Context context)
    {
        var go = GameObject.Instantiate(context.PlaceForUI);
        go.transform.position += Vector3.forward * 100;
        _playerInfoPanelUIController = new PlayerInfoPanelUIController(context, go);
        AddGameObject(go);
    }
}
