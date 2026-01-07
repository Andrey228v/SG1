using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Referens:")]
        [SerializeField] private InputReader _inputReader;
        
        private Unit _unit;
        private CameraController _cameraController;
        private Transform _spawnPoint;
        private Vector3 _respawnPosition;

        private ISaveLoadService _saveLoadService;
        private SignalBus _signalBus;

        [Inject]
        public void Initialize(CameraController cameraController, Transform spawnPoint, ISaveLoadService saveLoadService, SignalBus signalBus)
        {
            _cameraController = cameraController;
            transform.localPosition = spawnPoint.localPosition;
            _saveLoadService = saveLoadService;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            Debug.Log("ON ENABLE PC");

            _signalBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
            _signalBus.Subscribe<GameSavedSignal>(OnGameSave);

            _unit = transform.GetChild(0).GetComponent<Unit>();

            _unit.Awake();
            _cameraController.CameraCinemachine.Follow = _unit.PlayerView.transform;
            _cameraController.CameraCinemachine.LookAt = _unit.PlayerView.transform;
            _cameraController.CameraCinemachine.OnTargetObjectWarped(_unit.PlayerView.transform, _unit.PlayerView.transform.position - _cameraController.CameraCinemachine.transform.position - Vector3.forward);

            _inputReader.OnDirectionMoveChandged += _cameraController.MoveDirectionToCameraDirection;
            _cameraController.OnDirectionChanged += _unit.ProcessSignalDirection;
            _inputReader.OnMoved += _unit.SetProcessSignalMove;
            _inputReader.OnStoped += _unit.SetProcessSignalStop;
            _inputReader.OnJumpButtonDown += _unit.ProcessSignalJumpButtonDown;
            _inputReader.OnJumpButtonUp += _unit.ProcessSignalJumpButtonUp;

            LoadFromSave();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);
            _signalBus.Unsubscribe<GameSavedSignal>(OnGameSave);

            _inputReader.OnDirectionMoveChandged -= _cameraController.MoveDirectionToCameraDirection;
            _cameraController.OnDirectionChanged -= _unit.ProcessSignalDirection;
            _inputReader.OnMoved -= _unit.SetProcessSignalMove;
            _inputReader.OnStoped -= _unit.SetProcessSignalStop;
            _inputReader.OnJumpButtonDown -= _unit.ProcessSignalJumpButtonDown;
            _inputReader.OnJumpButtonUp -= _unit.ProcessSignalJumpButtonUp;

        }

        private void OnApplicationQuit()
        {
            SavePlayerData();
        }

        private void OnGameLoaded(GameLoadedSignal signal)
        {
            LoadFromSave();
        }

        private void OnGameSave(GameSavedSignal signal)
        {
            SavePlayerData();
        }

        private void SavePlayerData()
        {
            if (_saveLoadService.CurrentSave == null) return;

            _saveLoadService.CurrentSave.PlayerData.Position = new Vector3Serializable(_unit.transform.position);
            _saveLoadService.CurrentSave.PlayerData.Rotation = new Vector3Serializable(_unit.transform.eulerAngles);

            //_currentSave.PlayerData.Position = new Vector3Serializable(_unit.transform.position);
            //_currentSave.PlayerData.Rotation = new Vector3Serializable(_unit.transform.eulerAngles);

        }

        public void LoadFromSave()
        {
            if (_saveLoadService.CurrentSave == null) return;

            //var playerData = _currentSave.PlayerData;

            var playerData = _saveLoadService.CurrentSave.PlayerData;

            //Смысл в том, что обновляя каждый кадр мы затираем ....
            _unit.transform.position = playerData.Position.ToVector3();
            _respawnPosition = _unit.transform.position;


            // Загрузка позиции
            //if (!string.IsNullOrEmpty(playerData.CurrentCheckpointId))
            //{
            //    transform.position = playerData.Position.ToVector3();
            //    _respawnPosition = transform.position;
            //}

            // Загрузка здоровья
            //_health.SetHealth(playerData.Health);

            // Загрузка собранных монет
            //_coinCollector.SetCollectedCoins(playerData.CoinsCollected);
        }

        private void OnDeath()
        {
            _signalBus.Fire(new PlayerDiedSignal());
            Respawn();
        }

        public void SetCheckpoint(Vector3 position, string checkpointId)
        {
            _respawnPosition = position;
            _signalBus.Fire(new CheckpointActivatedSignal(checkpointId, position));
        }

        private void Respawn()
        {
            transform.position = _respawnPosition;
            //_health.ResetHealth();
            _signalBus.Fire(new PlayerRespawnedSignal());
        }

        public class Factory : PlaceholderFactory< PlayerController> { }
    }
}
