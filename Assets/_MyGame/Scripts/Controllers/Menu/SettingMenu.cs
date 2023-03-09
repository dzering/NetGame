using UnityEngine;
using Object = UnityEngine.Object;

public class SettingMenu : BaseController
{
    private SettingMenuUI _settingMenuUI;
    private SoundModel _soundModel;

    private Context _context;

    public SettingMenu(Context context, Transform placeForUI)
    {
        _context = context;
        _soundModel = context.GameModel.SoundModel;
        
        Init(_soundModel, placeForUI);
        UpdateUI(_context.GameModel.SoundModel);
    }

    private void Init(SoundModel soundModel, Transform placeForUI)
    {
        var go = Object.Instantiate(_context.UISO.SettingMenuGo, placeForUI);
        go.SetActive(true);

        if (!go.TryGetComponent(out _settingMenuUI))
            return;

        _settingMenuUI.Init(MusicOnOff, ChangeVolume, BackToMainMenu);
        AddGameObject(go);

    }

    public void BackToMainMenu()
    {
        _context.SaveDataRepository.Save(_soundModel);
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

    private void UpdateUI(SoundModel soundModel)
    {
        _settingMenuUI.UpdateUI(soundModel.IsMute, soundModel.VolumeMusic);
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }
}
