using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Context _context;
    private MainController _mainController;
    private SoundController _soundController;

    private void Start()
    { 
        _mainController = new MainController(_context);
        _soundController = new SoundController(_context);
        _context.GameModel.CurrentState = GameState.MainMenu;
    }
}