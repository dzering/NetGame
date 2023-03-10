using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public sealed class PlayerInfoPanelController : BaseController
{
    private PlayerInfoPanelUI[] _infoPanelsUI;
    private PlayerManager _target;
    private int mineNumber;

    public PlayerInfoPanelController(Context context)
    {
        var panelGo = Object.Instantiate(context.InfoPanelUI);
        _infoPanelsUI = panelGo.GetComponentsInChildren<PlayerInfoPanelUI>();

        AddGameObject(panelGo.gameObject);
        if (PhotonNetwork.IsMasterClient)
        {
            mineNumber = 0;
        }
        else
        {
            mineNumber = 1;
        }
    }

    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", _infoPanelsUI[mineNumber]);
            return;
        }

        _target = target;
        if(_infoPanelsUI == null)
            return;
        _infoPanelsUI[mineNumber].SetPlayerName(_target.photonView.Owner.NickName);
    }
}