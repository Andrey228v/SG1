using Assets.Scripts;
using System;
using UnityEngine;
using Zenject;


namespace Assets
{
    public class LevelStarter : IInitializable, IDisposable
    {
        private PlayerController.Factory _playerFactory;
        private Transform _trackingTarget;
        private CameraController _cameraController;
        //private Transform _spawnPoint;

        [Inject]
        public void Constructor(PlayerController.Factory playerFactory, CameraController cameraController)
        {
            _playerFactory = playerFactory;
            _cameraController = cameraController;
            //_spawnPoint = spawnPoint;
        }

        public void Initialize()
        {
            PlayerController player = _playerFactory.Create();
            //player.LoadFromSave();
            _trackingTarget = player.transform.GetChild(0);
            _cameraController.CameraCinemachine.Target.TrackingTarget = _trackingTarget;
        }

        public void Dispose()
        {
            
        }
    }
}
