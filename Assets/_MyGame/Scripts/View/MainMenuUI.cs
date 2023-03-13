using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _info;
    
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _startGame;
    [SerializeField] private Button _closeApplication;
    [SerializeField] private TMP_Text _doSomeFunText;
    [SerializeField] private Button _doSomeFunButton;
    
    
    public void Init(UnityAction settingAction, UnityAction startGame,
        UnityAction closeApplication, UnityAction doSomeFun, UnityAction<string> changeInput)
    {
        _startGame.onClick.AddListener(startGame);
        _settingButton.onClick.AddListener(settingAction);
        _closeApplication.onClick.AddListener(closeApplication);
        _doSomeFunButton.onClick.AddListener(doSomeFun);
    }

    private void OnDestroy()
    {
        _startGame.onClick.RemoveAllListeners();
        _settingButton.onClick.RemoveAllListeners();
        _closeApplication.onClick.RemoveAllListeners();
        _doSomeFunButton.onClick.RemoveAllListeners();
    }

    public void UpdateInfoText(string info)
    {
        _info.text = info;
    }

    public void UpdateTextFunButton(string text)
    {
        _doSomeFunText.text = text;
    }
}
