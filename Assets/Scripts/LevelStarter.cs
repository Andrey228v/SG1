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
        private Unit _unit;
        private Transform _spawnPoint;
        private SaveLoadService _saveLoadService;

        [Inject]
        public void Constructor(CameraController cameraController, Unit unit, [Inject(Id = "SpawnPoint")] Transform spawnPoint, SaveLoadService saveLoadService)
        {
            _cameraController = cameraController;
            _unit = unit;
            _spawnPoint = spawnPoint;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            _trackingTarget = _unit.transform;

            if (_saveLoadService.CurrentSave == null)
            {
                _unit.transform.position = _spawnPoint.position;
            }
            _cameraController.CameraCinemachine.Target.TrackingTarget = _trackingTarget;
        }

        public void Dispose()
        {
            
        }
    }
}
