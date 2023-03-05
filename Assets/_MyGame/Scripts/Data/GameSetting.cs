using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSetting
{
    private SoundDataSetting _soundData;
    public SoundDataSetting SoundData
    {
        get => _soundData;
        set => _soundData = value;
    }

    public GameSetting()
    {
        _soundData = new SoundDataSetting();
    }
}
