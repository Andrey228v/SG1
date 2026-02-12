using Assets.Scripts.Debugs;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using Assets.Scripts.Utilites;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour, IPause
    {
        [Header("Referens:")]
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Transform _testPosition; //позиция для тестов...
        [SerializeField] private Transform _testObj; //тестовый объект...

        private Unit _unit;
        private CameraController _cameraController;
        private Vector3 _respawnPosition;
        private Transform _spawnPoint;
        private UnitDebug _unitDebug;

        private ISaveLoadService _saveLoadService;
        private SignalBus _signalBus;

        [Inject]
        public void Initialize(CameraController cameraController,
            [Inject(Id = "SpawnPoint")] Transform spawnPoint,
            ISaveLoadService saveLoadService,
            SignalBus signalBus,
            UnitDebug unitDebug,
            Unit unit
            )
        {
            _cameraController = cameraController;
            _saveLoadService = saveLoadService;
            _signalBus = signalBus;
            _unitDebug = unitDebug;
            _unit = unit;
            _spawnPoint = spawnPoint;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameSavedSignal>(OnGameSave);
            _signalBus.Subscribe<GameLoadedSignal>(OnGameLoaded);

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
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameSavedSignal>(OnGameSave);
            _signalBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);

            _inputReader.OnDirectionMoveChandged -= _cameraController.MoveDirectionToCameraDirection;
            _cameraController.OnDirectionChanged -= _unit.ProcessSignalDirection;
            _inputReader.OnMoved -= _unit.SetProcessSignalMove;
            _inputReader.OnStoped -= _unit.SetProcessSignalStop;
            _inputReader.OnJumpButtonDown -= _unit.ProcessSignalJumpButtonDown;
            _inputReader.OnJumpButtonUp -= _unit.ProcessSignalJumpButtonUp;

        }

        private void Start()
        {
            _unitDebug.SetUnit(_unit);
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
            if (_saveLoadService.CurrentSave == null)
            {
                return;
            }

            _saveLoadService.CurrentSave.playerSaveData.Position = new Vector3Serializable(_unit.transform.position);
            _saveLoadService.CurrentSave.playerSaveData.Rotation = new Vector3Serializable(_unit.transform.eulerAngles);
        }

        public void LoadFromSave()
        {
            if (_saveLoadService.IsFirstLoad())
            {
                _unit.transform.position = _spawnPoint.position;
                return;
            }

            var playerData = _saveLoadService.CurrentSave.playerSaveData;
            _unit.transform.position = playerData.Position.ToVector3();

            _testObj.transform.position = playerData.Position.ToVector3(); // test....
        }

        public void Test() // test....
        {
            _unit.transform.position = _testPosition.position; // test....
            _testObj.transform.position = _testPosition.position; // test....
        }

        //private void OnDeath()
        //{
        //    _signalBus.Fire(new PlayerDiedSignal());
        //    Respawn();
        //}

        public void SetCheckpoint(Vector3 position, string checkpointId)
        {
            //_respawnPosition = position;
            //_signalBus.Fire(new CheckpointActivatedSignal(checkpointId, position));
        }

        private void Respawn()
        {
            //transform.position = _respawnPosition;
            ////_health.ResetHealth();
            //_signalBus.Fire(new PlayerRespawnedSignal());
        }

        public void Pause()
        {

        }

        public void Continue()
        {
            
        }

        

        //public class Factory : PlaceholderFactory<PlayerController> { }
    }
}
