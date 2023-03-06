    using System;
    using UnityEngine;

    public sealed class Context : MonoBehaviour
    {
        [field: SerializeField] public UISO UISO { get; private set; }
        [field: SerializeField] public Transform PlaceForUI { get; private set; }

        [SerializeField] private AudioSource _audioSource;

        private GameModel _gameGameModel;

        public GameModel GameModel
        {
            get => _gameGameModel;
            set => _gameGameModel = value;
        }

        public AudioSource Source => _audioSource;

        private void Awake()
        {
            var soundModel = new SoundModel();
            _gameGameModel = new GameModel(soundModel);
        }
    }