using UnityEngine;

public class PlayerPrefsData : IData<SoundModel>
{
    public void Save(SoundModel data, string path = null)
    {
        PlayerPrefs.SetFloat("Volume", data.VolumeMusic);

        var isMute = data.IsMute;
        PlayerPrefs.SetInt("IsMute", isMute ? 1 : 0);

        PlayerPrefs.Save();
    }

    public SoundModel Load(string path = null)
    {
        var result = new SoundModel();
        var key = "Volume";
        if (PlayerPrefs.HasKey(key))
            result.VolumeMusic = PlayerPrefs.GetFloat(key);


        key = "IsMute";
        if (PlayerPrefs.HasKey(key))
            result.IsMute = PlayerPrefs.GetInt(key) == 1 ? true : false;

        return result;
    }

}