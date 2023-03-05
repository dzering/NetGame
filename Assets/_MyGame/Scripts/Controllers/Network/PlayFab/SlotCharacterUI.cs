using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SlotCharacterUI : MonoBehaviour
{
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _fillSlot;

    [SerializeField] private Button _createCharacter;
    [SerializeField] private Button _selectCharacter;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _exp;
    [SerializeField] private TMP_Text _level;

    private void Start()
    {
        _emptySlot.SetActive(true);
        _fillSlot.SetActive(false);
    }
    private void OnDestroy()
    {
       RemoveAllListener();
    }

    public void SubscrideOnButtonCreateCharacter(UnityAction createCharacter)
    {
        _createCharacter.onClick.AddListener(createCharacter);
    }

    public void SubscribeOnButtonSelectCharacter(UnityAction selectCharacter)
    {
        _selectCharacter.onClick.AddListener(selectCharacter);
    }

    public void UnsubscribeOnButtonSelectCharacter(UnityAction selectCharacter)
    {
        _selectCharacter.onClick.RemoveListener(selectCharacter);
    }

    public void UnsubscribeOnButtonCreateCharacter(UnityAction createChatacter)
    {
        _createCharacter.onClick.RemoveListener(createChatacter);
    }

    public void RemoveAllListener()
    {
        _selectCharacter.onClick.RemoveAllListeners();
        _createCharacter.onClick.RemoveAllListeners();
    }

    public void ShowFillSlot(string name, string exp, string level)
    {
        _emptySlot.gameObject.SetActive(false);
        _fillSlot.gameObject.SetActive(true);
        _name.text = name;
        _exp.text = exp;
        _level.text = level;
    }

    public void ClearData()
    {
        _emptySlot.SetActive(true);
        _fillSlot.SetActive(false);
        _name.text = "";
        _exp.text = "";
        _level.text = "";
    }
}

 
