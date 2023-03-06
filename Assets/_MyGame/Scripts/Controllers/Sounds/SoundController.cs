using UnityEngine;

public sealed class SoundController : BaseController
{
    private AudioSource _audioSource;
    private Context _context;
    public SoundController(Context context)
    {
        _audioSource = context.Source;
        // context.GameModel.SoundModel.OnChangeVolume += ChangeVolume;
        // context.GameModel.SoundModel.OnChangeMute += Mute;
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

    public void ChangeVolume(float value)
    {
        _audioSource.volume = value;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        // _context.GameModel.SoundModel.OnChangeMute -= Mute;
        // _context.GameModel.SoundModel.OnChangeVolume -= ChangeVolume;
    }
}