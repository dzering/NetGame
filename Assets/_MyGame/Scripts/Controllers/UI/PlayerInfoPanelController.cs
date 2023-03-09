using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInfoPanelController : BaseController
{
    private PlayerInfoPanelUI _infoPanelUI;
    public PlayerInfoPanelController(Context context)
    {
        var panelGo = Object.Instantiate(context.InfoPanelUI);
        _infoPanelUI = panelGo.GetComponent<PlayerInfoPanelUI>();
        
        AddGameObject(panelGo.gameObject);
    }
}