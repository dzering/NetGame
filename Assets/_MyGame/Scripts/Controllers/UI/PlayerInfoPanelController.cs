using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInfoPanelController : BaseController
{
    private PlayerInfoPanelUI _infoPanelUI;
    public PlayerInfoPanelController(Context context, Transform placeForUI)
    {
        var panelGo = Object.Instantiate(context.InfoPanelUI, placeForUI);
        _infoPanelUI = panelGo.GetComponent<PlayerInfoPanelUI>();
        
        AddGameObject(panelGo.gameObject);
    }
}