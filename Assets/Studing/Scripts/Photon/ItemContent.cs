using System;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemContent : MonoBehaviour
{
    public event Action<string> OnConnectRoom;
    [SerializeField] private TMP_Text _itemText;
    [SerializeField] private Button _applyButton;

    public void UpdateText(string itemContext)
    {
        _itemText.text = itemContext;
    }

    private void Start()
    {
        _applyButton.onClick.AddListener(ApplyRoom);
    }

    private void OnDestroy()
    {
        OnConnectRoom = null;
        _applyButton.onClick.RemoveAllListeners();
    }

    private void ApplyRoom()
    {
        OnConnectRoom?.Invoke(_itemText.text);
    }
}
