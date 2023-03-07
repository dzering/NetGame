    using System;
    using PlayFab;
    using UnityEngine;

    public sealed class Context : MonoBehaviour
    {
        #region Fields

        [field: SerializeField] public UISO UISO { get; private set; }
        [field: SerializeField] public Transform PlaceForUI { get; private set; }
        [field: SerializeField] public PlayFabAccountManager PlayFabAccount { get; private set; }
        [field: SerializeField] public WishConfig WishConfig { get; private set; }
        [SerializeField] private AudioSource _audioSource;
       
        private GameModel _gameGameModel;
        [HideInInspector] public SaveDataRepository SaveDataRepository;

        #endregion

        #region Properties

        public GameModel GameModel
        {
            get => _gameGameModel;
            set => _gameGameModel = value;
        }

        public AudioSource Source => _audioSource;

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