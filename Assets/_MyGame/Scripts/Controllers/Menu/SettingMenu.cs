using UnityEngine;
using Object = UnityEngine.Object;

public class SettingMenu : BaseController
{
    private SettingMenuUI _settingMenuUI;
    private SoundModel _soundModel;

    private Context _context;

    public SettingMenu(Context context)
    {
        _context = context;
        _soundModel = context.GameModel.SoundModel;
        
        Init(_soundModel);
    }

    private void Init(SoundModel soundModel)
    {
        var go = Object.Instantiate(_context.UISO.SettingMenuGo, _context.PlaceForUI);
        go.SetActive(true);

        if (!go.TryGetComponent(out _settingMenuUI))
            return;

        _settingMenuUI.Init(MusicOnOff, ChangeVolume, BackToMainMenu);
        AddGameObject(go);

    }

    public void BackToMainMenu()
    {
        _context.GameModel.CurrentState = GameState.MainMenu;
    }

    public void MusicOnOff(bool isMute)
    {
        _soundModel.IsMute = isMute;
    }
    
    public void ChangeVolume(float value)
    {
        _soundModel.VolumeMusic = value;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }
}
