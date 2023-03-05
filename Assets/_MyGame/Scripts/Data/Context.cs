    using System;
    using UnityEngine;

    public sealed class Context : MonoBehaviour
    {
        [field: SerializeField] public UISO UISO { get; private set; }
        [field: SerializeField] public Transform PlaceForUI { get; private set; }

        private GameModel _gameGameModel;

        public GameModel GameModel
        {
            get => _gameGameModel;
            set => _gameGameModel = value;
        }

        private void Awake()
        {
            _gameGameModel = new GameModel();
        }
    }