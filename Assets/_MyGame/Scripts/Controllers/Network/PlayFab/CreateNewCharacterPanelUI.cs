using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateNewCharacterPanelUI : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private TMP_InputField _inputField;


    private void Start()
    {
        _returnButton.onClick.AddListener(Return);
    }

    public void SubscribeOnInputFieldChanged(UnityAction<string> action)
    {
        _inputField.onValueChanged.AddListener(action);
    }

    private void OnDestroy()
    {
        _returnButton.onClick.RemoveAllListeners();
    }

    public void SubscribeOnCreateButton(UnityAction createAction)
    {
        _createButton.onClick.AddListener(createAction);
    }

    public void UnsubscribeOnCreateButton(UnityAction createButton)
    {
        _createButton.onClick.RemoveListener(createButton);
    }

    private void Return()
    {
        this.gameObject.SetActive(false);
    }
    
}
