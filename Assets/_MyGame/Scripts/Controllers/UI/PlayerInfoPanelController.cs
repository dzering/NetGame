using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public sealed class PlayerInfoPanelController : BaseController
{
    private PlayerInfoPanelUI _infoPanelUI;
    private PlayerManager _target;
    public PlayerInfoPanelController(Context context)
    {
        var panelGo = Object.Instantiate(context.InfoPanelUI);
        _infoPanelUI = panelGo.GetComponent<PlayerInfoPanelUI>();

        AddGameObject(panelGo.gameObject);
    }

    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", _infoPanelUI);
            return;
        }

        _target = target;
        _infoPanelUI.SetPlayerName(_target.photonView.Owner.NickName);
    }
}