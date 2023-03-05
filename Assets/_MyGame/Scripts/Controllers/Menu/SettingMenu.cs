using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

public class SettingMenu
{
    public event Action<SoundDataSetting> OnCloseSettingMenu;
    private readonly SettingMenuUI _settingMenuUI;
    private readonly SoundDataSetting _soundData;

    public SettingMenu(SettingMenuUI settingMenuUI, SoundDataSetting soundData)
    {
        _settingMenuUI = settingMenuUI;
        _soundData = soundData;
        
        _settingMenuUI.Init(MusicOnOff, ChangeVolume, BackToMainMenu);
        GameStateManager.Instance.OnSettingMenu += OpenSettingMenu;
    }

    public void OpenSettingMenu()
    {
        _settingMenuUI.gameObject.SetActive(true);
        
        _settingMenuUI.UpdateUI(_soundData.IsMusicOn, _soundData.MusicVolume);
    }

    public void BackToMainMenu()
    {
        _settingMenuUI.gameObject.SetActive(false);
        GameStateManager.Instance.CurrentState = GameState.MainMenu;
        OnCloseSettingMenu?.Invoke(_soundData);
    }

    public void MusicOnOff(bool isMusic)
    {
        _soundData.IsMusicOn = isMusic;
    }

    public void ChangeVolume(float value)
    {
        _soundData.MusicVolume = value;
    }

}
