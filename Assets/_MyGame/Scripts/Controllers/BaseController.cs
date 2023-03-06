using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _MyGame.Scripts.Controllers
{
    public abstract class BaseController : IDisposable
    {
        private bool _isDisposed;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private readonly List<GameObject> _gameObjects = new List<GameObject>();

        public void Dispose()
        {
            if(_isDisposed)
                return;
            
            _isDisposed = true;
            
            DisposeChildDisposable();
            DestroyedChildGameObjects();
            
            OnDispose();
        }

        private void DestroyedChildGameObjects()
        {
            foreach (var gameObject in _gameObjects)
            {
                Object.Destroy(gameObject);
            }

            _gameObjects.Clear();
        }

        private void DisposeChildDisposable()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }

        protected void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }
        
        protected virtual void OnDispose(){}
    }
}