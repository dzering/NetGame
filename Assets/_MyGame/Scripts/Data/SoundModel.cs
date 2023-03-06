using System;

public sealed class SoundModel
{
    public event Action<bool> OnChangeMute;
    public event Action<float> OnChangeVolume;
    private bool _isMute;
    private float _volumeMusic;
    public bool IsMute
    {
        get => _isMute;
        set
        {
            if(_isMute == value)
                return;
                     
            _isMute = value;
            OnChangeMute?.Invoke(value);
        }
    }
    public float VolumeMusic
    {
        get => _volumeMusic;
        set
        {
            _volumeMusic = value;
            OnChangeVolume?.Invoke(value);
        }
    }
}