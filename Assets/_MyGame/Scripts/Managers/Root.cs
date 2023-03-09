using System;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Context _context;
    
    private Transform _placeForUI;
    private MainController _mainController;
    private SoundController _soundController;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _placeForUI = Instantiate(_context.PlaceForUI).transform;
        DontDestroyOnLoad(_placeForUI);
        _mainController = new MainController(_context, _placeForUI);
        _soundController = new SoundController(_context);

        _context.GameModel.SoundModel.OnChangeMute += _soundController.Mute;
        _context.GameModel.SoundModel.OnChangeVolume += _soundController.ChangeVolume;
        _context.SaveDataRepository.Load(_context.GameModel.SoundModel);
        
        _context.GameModel.CurrentState = GameState.MainMenu;
    }
}