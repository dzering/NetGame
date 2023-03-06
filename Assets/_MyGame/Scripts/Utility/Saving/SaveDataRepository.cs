    using UnityEngine;

    public sealed class SaveDataRepository
    {
        private IData<SavedData> _data;

        public SaveDataRepository()
        {
            _data = new PlayerPrefsData();
        }

        // public void Save(SoundDataSetting soundData)
        // {
        //     var savedData = new SavedData
        //     {
        //         Volume = soundData.MusicVolume,
        //         IsMute = soundData.IsMusicOn
        //     };
        //     _data.Save(savedData);
        //     Debug.Log($"Data Saved: {soundData.MusicVolume} {soundData.IsMusicOn}");
        // }
        //
        // public void Load(SoundDataSetting soundData)
        // {
        //     var data = _data.Load();
        //     soundData.MusicVolume = data.Volume;
        //     soundData.IsMusicOn = data.IsMute;
        //     
        //     Debug.Log($"Data Load {data.Volume} {data.IsMute}");
        // }
    }