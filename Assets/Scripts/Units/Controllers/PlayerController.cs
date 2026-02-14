using Assets.Scripts.Debugs;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using Assets.Scripts.Utilites;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PlayerController :IInitializable, IDisposable, IPause
    {
        private UnitSignalReader _unitSignalReader;
        private CameraController _cameraController;
        private Vector3 _respawnPosition;
        private Transform _spawnPoint;
        private UnitDebug _unitDebug;
        private InputReader _inputReader;

        private ISaveLoadService _saveLoadService;
        private SignalBus _signalBus;

        public PlayerController(CameraController cameraController,
            [Inject(Id = "SpawnPoint")] Transform spawnPoint,
            ISaveLoadService saveLoadService,
            SignalBus signalBus,
            UnitDebug unitDebug,
            UnitSignalReader unitSignalReader,
            InputReader inputReader
            )
        {
            _cameraController = cameraController;
            _saveLoadService = saveLoadService;
            _signalBus = signalBus;
            _unitDebug = unitDebug;
            _unitSignalReader = unitSignalReader;
            _spawnPoint = spawnPoint;
            _inputReader = inputReader;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameSavedSignal>(OnGameSave);
            _signalBus.Subscribe<GameLoadedSignal>(OnGameLoaded);

            _cameraController.CameraCinemachine.Follow = _unitSignalReader.PlayerView.CharacterView.transform;
            _cameraController.CameraCinemachine.LookAt = _unitSignalReader.PlayerView.CharacterView.transform;
            _cameraController.CameraCinemachine.OnTargetObjectWarped(_unitSignalReader.PlayerView.CharacterView.transform, _unitSignalReader.PlayerView.CharacterView.transform.position - _cameraController.CameraCinemachine.transform.position - Vector3.forward);

            _inputReader.OnDirectionMoveChandged += _cameraController.MoveDirectionToCameraDirection;
            _cameraController.OnDirectionChanged += _unitSignalReader.ProcessSignalDirection;
            _inputReader.OnMoved += _unitSignalReader.SetProcessSignalMove;
            _inputReader.OnStoped += _unitSignalReader.SetProcessSignalStop;
            _inputReader.OnJumpButtonDown += _unitSignalReader.ProcessSignalJumpButtonDown;
            _inputReader.OnJumpButtonUp += _unitSignalReader.ProcessSignalJumpButtonUp;

            _unitDebug.SetUnit(_unitSignalReader);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameSavedSignal>(OnGameSave);
            _signalBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);

            _inputReader.OnDirectionMoveChandged -= _cameraController.MoveDirectionToCameraDirection;
            _cameraController.OnDirectionChanged -= _unitSignalReader.ProcessSignalDirection;
            _inputReader.OnMoved -= _unitSignalReader.SetProcessSignalMove;
            _inputReader.OnStoped -= _unitSignalReader.SetProcessSignalStop;
            _inputReader.OnJumpButtonDown -= _unitSignalReader.ProcessSignalJumpButtonDown;
            _inputReader.OnJumpButtonUp -= _unitSignalReader.ProcessSignalJumpButtonUp;

        }

        //private void Start()
        //{
        //    _unitDebug.SetUnit(_unitSignalReader);
        //}

        //private void OnApplicationQuit()
        //{
        //    SavePlayerData();
        //}

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
            if (_saveLoadService.CurrentSave == null)
            {
                return;
            }

            _saveLoadService.CurrentSave.playerSaveData.Position = new Vector3Serializable(_unitSignalReader.PlayerView.CharacterView.transform.position);
            _saveLoadService.CurrentSave.playerSaveData.Rotation = new Vector3Serializable(_unitSignalReader.PlayerView.CharacterView.transform.eulerAngles);
        }

        public void LoadFromSave()
        {
            if (_saveLoadService.IsFirstLoad())
            {
                _unitSignalReader.PlayerView.CharacterView.transform.position = _spawnPoint.position;
                return;
            }

            var playerData = _saveLoadService.CurrentSave.playerSaveData;
            _unitSignalReader.PlayerView.CharacterView.transform.position = playerData.Position.ToVector3();

            //_testObj.transform.position = playerData.Position.ToVector3(); // test....
        }

        //public void Test() // test....
        //{
        //    _unitSignalReader.PlayerView.CharacterView.transform.position = _testPosition.position; // test....
        //    _testObj.transform.position = _testPosition.position; // test....
        //}

        public void Pause()
        {
            _cameraController.Pause();
        }

        public void Continue()
        {
            _cameraController.Continue();
        }
    }
}
