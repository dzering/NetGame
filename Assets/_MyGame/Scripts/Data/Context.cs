using UnityEngine;

public sealed class Context : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public UISO UISO { get; private set; }
        [field: SerializeField] public GameObject PlaceForUI { get; private set; }
        [field: SerializeField] public PlayFabAccountManager PlayFabAccount { get; private set; }
        [SerializeField] private GameObject _photonPhotonGameManager;
        [field: SerializeField] public WishConfig WishConfig { get; private set; }
        [SerializeField] private AudioSource _audioSource;
        
        [SerializeField] private PhotonLauncher _photonPhotonLauncher;
       
        private GameModel _gameGameModel;
        [HideInInspector] public SaveDataRepository SaveDataRepository;

        [SerializeField] private GameObject _playerInfoPanelUI;

        #endregion

        #region Properties

        public GameModel GameModel
        {
            get => _gameGameModel;
            set => _gameGameModel = value;
        }

        public AudioSource Source => _audioSource;

        public PhotonLauncher PhotonLauncher => _photonPhotonLauncher;

        public GameObject InfoPanelUI => _playerInfoPanelUI;

        public GameObject PhotonGameManager => _photonPhotonGameManager;
        

        #endregion

        #region UnityMethods

        private void Awake()
        {
            var soundModel = new SoundModel();
            _gameGameModel = new GameModel(soundModel);
            SaveDataRepository = new SaveDataRepository();
        }

        #endregion
    }