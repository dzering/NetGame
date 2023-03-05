using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private SoundDataSetting _soundData;
    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        _soundData.OnChangeValue += ChangeVolume;
        _soundData.OnMusicOff += Mute;
        DontDestroyOnLoad(this);
    }

    public void Play()
    {
        //_soundData = GameStateManager.Instance.GameSetting.SoundData;
        _audioSource.Play();
    }

    public void StopPlay()
    {
        _audioSource.Stop();
    }

    public void Mute(bool isMute)
    {
        _audioSource.mute = isMute;
    }

    public void ChangeVolume(float value)
    {
        _audioSource.volume = value;
    }
}
