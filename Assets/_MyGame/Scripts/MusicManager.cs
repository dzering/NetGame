using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Play()
    {
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
}
