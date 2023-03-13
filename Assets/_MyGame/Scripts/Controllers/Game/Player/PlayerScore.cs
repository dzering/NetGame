using Photon.Pun;
using UnityEngine;

public class PlayerScore : MonoBehaviourPun
{
    private PlayerManager _target;
    private float _score;
    
    public void SetTarget(PlayerManager target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        _target = target;
        _target.Score = Score;
    }

    public float Score
    {
        get => _score;
        set
        {
            _score = value;
            _target.Score = _score;
            Debug.Log(value);
        }
    }

    public void AddScore(float score)
    {
        Score += score;
    }

    public void LoadScore(float score)
    {
        Score = score;
    }

    public void SaveScore()
    {
        
    }
}
