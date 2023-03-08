using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameLable;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Slider _healthSlider;

    public void SetPlayerName(string name)
    {
        _playerNameLable.text = name;
    }

    public void ChangeHealth(float currentHealth)
    {
        _healthSlider.value = currentHealth;
    }

    public void ChangeScore(int score)
    {
        _score.text = "SCORE: " + score;
    }
}