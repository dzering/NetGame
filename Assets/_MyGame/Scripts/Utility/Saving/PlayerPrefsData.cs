using UnityEngine;

public class PlayerPrefsData : IData<SavedData>
{
    public void Save(SavedData data, string path = null)
    {
        PlayerPrefs.SetFloat("Volume", data.Volume);

        var isMute = data.IsMute;
        PlayerPrefs.SetInt("IsMute", isMute ? 1 : 0);

        PlayerPrefs.Save();
    }

    public SavedData Load(string path = null)
    {
        var result = new SavedData();
        var key = "Volume";
        if (PlayerPrefs.HasKey(key))
            result.Volume = PlayerPrefs.GetFloat(key);


        key = "IsMute";
        if (PlayerPrefs.HasKey(key))
            result.IsMute = PlayerPrefs.GetInt(key) == 1 ? true : false;

        return result;
    }

}