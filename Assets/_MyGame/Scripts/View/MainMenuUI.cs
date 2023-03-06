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
    [SerializeField] private Button _closeApplication;
    public void Init(UnityAction settingAction, UnityAction startGame,
        UnityAction closeApplication)
    {
        _startGame.onClick.AddListener(startGame);
        _settingButton.onClick.AddListener(settingAction);
        _closeApplication.onClick.AddListener(closeApplication);
    }

    private void OnDestroy()
    {
        _startGame.onClick.RemoveAllListeners();
        _settingButton.onClick.RemoveAllListeners();
        _closeApplication.onClick.RemoveAllListeners();
    }

    public void UpdateInfoText(string info)
    {
        _info.text = info;
    }
}
