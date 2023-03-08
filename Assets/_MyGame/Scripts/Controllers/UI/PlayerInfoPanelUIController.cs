using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInfoPanelUIController : BaseController
{
    private PlayerInfoPanelUI _infoPanelUI;
    
    public PlayerInfoPanelUIController(Context context, GameObject placeForUI)
    {
        var panelGo = Object.Instantiate(context.InfoPanelUI, placeForUI.transform);
        _infoPanelUI = panelGo.GetComponent<PlayerInfoPanelUI>();
        
        AddGameObject(panelGo.gameObject);
    }
}