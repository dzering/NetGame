using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

public class SettingMenu
{
    public event Action<SoundDataSetting> OnCloseSettingMenu;
    private SettingMenuUI _settingMenuUI;
    private SoundDataSetting _soundData;

    private Context _context;

    public SettingMenu(Context context, SoundDataSetting soundData)
    {
        Init(context, soundData);
        // GameStateManager.Instance.OnSettingMenu += OpenSettingMenu;
    }

    private void Init(Context context, SoundDataSetting soundData)
    {
        _context = context;
        _soundData = soundData;

        var go = Object.Instantiate(_context.UISO.SettingMenuGo, _context.PlaceForUI);
        go.SetActive(true);

        if (!go.TryGetComponent(out _settingMenuUI))
            return;

        _settingMenuUI.Init(MusicOnOff, ChangeVolume, BackToMainMenu);
    }


    public void OpenSettingMenu()
    {
        _settingMenuUI.gameObject.SetActive(true);
        
        _settingMenuUI.UpdateUI(_soundData.IsMusicOn, _soundData.MusicVolume);
    }

    public void BackToMainMenu()
    {
        _settingMenuUI.gameObject.SetActive(false);
       // GameStateManager.Instance.CurrentState = GameState.MainMenu;
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
