    using System;
    using PlayFab;
    using UnityEngine;
    using UnityEngine.Serialization;

    public sealed class Context : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public UISO UISO { get; private set; }
        [field: SerializeField] public GameObject PlaceForUI { get; private set; }
        [field: SerializeField] public PlayFabAccountManager PlayFabAccount { get; private set; }
        [field: SerializeField] public WishConfig WishConfig { get; private set; }
        [SerializeField] private AudioSource _audioSource;
        
        [FormerlySerializedAs("_photonLauncher")] [SerializeField] private PhotonLauncher _photonPhotonLauncher;
       
        private GameModel _gameGameModel;
        [HideInInspector] public SaveDataRepository SaveDataRepository;

        [SerializeField] private PlayerInfoPanelUI _playerInfoPanelUI;

        #endregion

        #region Properties

        public GameModel GameModel
        {
            get => _gameGameModel;
            set => _gameGameModel = value;
        }

        public AudioSource Source => _audioSource;

        public PhotonLauncher PhotonLauncher => _photonPhotonLauncher;

        public PlayerInfoPanelUI InfoPanelUI => _playerInfoPanelUI;

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