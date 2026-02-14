using Assets.Scripts;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using System;
using UnityEngine;
using Zenject;

namespace Assets
{
    public class LevelStarter : IInitializable, IDisposable
    {
        private Transform _trackingTarget;
        private CameraController _cameraController;
        private UnitSignalReader _unitSignalReader;
        private Transform _spawnPoint;
        private SaveLoadService _saveLoadService;

        [Inject]
        public void Constructor(CameraController cameraController, UnitSignalReader unitSignalReader, [Inject(Id = "SpawnPoint")] Transform spawnPoint, SaveLoadService saveLoadService)
        {
            _cameraController = cameraController;
            _unitSignalReader = unitSignalReader;
            _spawnPoint = spawnPoint;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            _trackingTarget = _unitSignalReader.PlayerView.CharacterView.transform;

            if (_saveLoadService.IsFirstLoad())
            {
                _unitSignalReader.PlayerView.CharacterView.transform.position = _spawnPoint.position;
            }
            _cameraController.CameraCinemachine.Target.TrackingTarget = _trackingTarget;
        }

        public void Dispose()
        {
            
        }
    }
}
