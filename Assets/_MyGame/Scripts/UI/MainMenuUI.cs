using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _info;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _startGame;

    public void Init(UnityAction settingAction, UnityAction startGame, string info)
    {
        _startGame.onClick.AddListener(startGame);
        _settingButton.onClick.AddListener(settingAction);
        _info.text = info;
    }

    private void OnDestroy()
    {
        _startGame.onClick.RemoveAllListeners();
        _settingButton.onClick.RemoveAllListeners();
        _info.text = "";
    }
}
