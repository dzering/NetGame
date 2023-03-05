using System;

public class SoundDataSetting
{
    public Action<bool> OnMusicOff;
    public Action<float> OnChangeValue;
    
    private float _musicVolume;
    private bool _isMusicOn;

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            OnChangeValue?.Invoke(value);
        }
    }

    public bool IsMusicOn
    {
        get => _isMusicOn;
        set
        {
            _isMusicOn = value;
            OnMusicOff?.Invoke(value);
        }
    }
}