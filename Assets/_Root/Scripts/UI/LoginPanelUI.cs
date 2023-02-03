using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LoginPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _panelNameText;

    [SerializeField] private Button _connectionButton;
    [SerializeField] private TextMeshProUGUI _buttonConnectionText;

    [SerializeField] private Button _disconnectionButton;


    public void Init(UnityAction action, string panelNameText)
    {
        _connectionButton.onClick.AddListener(action);
        _connectionButton.gameObject.SetActive(true);
        _panelNameText.text = panelNameText;
    }

    public void InitDisconnect(UnityAction action)
    {
        _disconnectionButton.onClick.AddListener(action);
        _disconnectionButton.gameObject.SetActive(true);
    }

    public void ChangeButtonView(Color color, string text)
    {
        _connectionButton.image.color = color;
        _buttonConnectionText.text = text;
    }

    private void OnDestroy()
    {
        _connectionButton.onClick.RemoveAllListeners();
        _disconnectionButton.onClick.RemoveAllListeners();
    }
}
