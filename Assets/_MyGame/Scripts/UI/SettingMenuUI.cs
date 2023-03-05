using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingMenuUI : MonoBehaviour
{
    [SerializeField] private Toggle _musicOffOn;
    [SerializeField] private Slider _musicVolume;
    [SerializeField] private Button _returnButton;

    public void Init(UnityAction<bool> musicOffOn, UnityAction<float> changeVolume, UnityAction goBack)
    {
        _musicOffOn.onValueChanged.AddListener(musicOffOn);
        _musicVolume.onValueChanged.AddListener(changeVolume);
        _returnButton.onClick.AddListener(goBack);
    }

    public void UpdateUI(bool isMute, float volume)
    {
        _musicOffOn.isOn = isMute;
        _musicVolume.value = volume;

    }

    private void OnDestroy()
    {
        _musicVolume.onValueChanged.RemoveAllListeners();
        _musicOffOn.onValueChanged.RemoveAllListeners();
        _returnButton.onClick.RemoveAllListeners();
    }
}