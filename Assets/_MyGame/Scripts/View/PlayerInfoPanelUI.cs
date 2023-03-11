using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerNameLable;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Slider _healthSlider;

    private PlayerManager _target;


    private void Awake()
    {
        {
            this.transform.SetParent(GameObject.Find("PlayerInfoPanels Canvas").GetComponent<Transform>(), false);
        }
    }

    void Update()
    {
        // Reflect the Player Health
        if (_healthSlider != null)
        {
            _healthSlider.value = _target.Health;
        }
        
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    
    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        _target = target;
        if (_playerNameLable != null)
        {
            _playerNameLable.text = target.photonView.Owner.NickName;
        }

        SetMaxHealth(_target.Health);
    }

    public void SetPlayerName(string name)
    {
        _playerNameLable.text = name;
    }

    private void SetMaxHealth(float maxHealth)
    {
        _healthSlider.maxValue = _target.Health;
    }

    public void ChangeScore(int score)
    {
        _score.text = "SCORE: " + score;
    }
}