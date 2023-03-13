using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Context _context;
    
    private Transform _placeForUI;
    private MainController _mainController;
    private SoundController _soundController;

    public Context Context => _context;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _placeForUI = Instantiate(Context.PlaceForUI).transform;
        DontDestroyOnLoad(_placeForUI);
        _mainController = new MainController(Context, _placeForUI);
        _soundController = new SoundController(Context);

        Context.GameModel.SoundModel.OnChangeMute += _soundController.Mute;
        Context.GameModel.SoundModel.OnChangeVolume += _soundController.ChangeVolume;
        Context.SaveDataRepository.Load(Context.GameModel.SoundModel);
        
        Context.GameModel.CurrentState = GameState.MainMenu;
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        _mainController = null;
    }
}