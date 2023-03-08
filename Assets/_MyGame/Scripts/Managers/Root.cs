using System;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Context _context;
    private MainController _mainController;
    private SoundController _soundController;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    { 
        _mainController = new MainController(_context);
        _soundController = new SoundController(_context);

        _context.GameModel.SoundModel.OnChangeMute += _soundController.Mute;
        _context.GameModel.SoundModel.OnChangeVolume += _soundController.ChangeVolume;
        _context.SaveDataRepository.Load(_context.GameModel.SoundModel);
        
        _context.GameModel.CurrentState = GameState.MainMenu;
    }
}