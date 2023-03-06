    using UnityEngine;

    public sealed class SaveDataRepository
    {
        private IData<SoundModel> _data;

        public SaveDataRepository()
        {
            _data = new PlayerPrefsData();
        }

        public void Save(SoundModel soundData)
        {
            // var savedData = new SavedData
            // {
            //     Volume = soundData.MusicVolume,
            //     IsMute = soundData.IsMusicOn
            // };
            _data.Save(soundData);
            Debug.Log($"Data Saved: {soundData.VolumeMusic} {soundData.IsMute}");
        }
        
        public void Load(SoundModel soundData)
        {
            var data = _data.Load();
            soundData.VolumeMusic = data.VolumeMusic;
            soundData.IsMute = data.IsMute;
            
            Debug.Log($"Data Load {data.VolumeMusic} {data.IsMute}");
        }
    }