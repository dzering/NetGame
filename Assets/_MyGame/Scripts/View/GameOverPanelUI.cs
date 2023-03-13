using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanelUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _resultText;

    public Button ExitButton
    {
        get => _exitButton;
        set => _exitButton = value;
    }

    public TMP_Text ResultText
    {
        get => _resultText;
        set => _resultText = value;
    }

    private void Awake()
    {
        this.transform.SetParent(GameObject.Find("PlayerInfoPanels Canvas").GetComponent<Transform>(), false);
    }
    
}
