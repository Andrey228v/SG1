using Assets.Scripts.DetectorProperties;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts
{
    //https://github.com/adammyhre/3D-Platformer/blob/master/Assets/_Project/Scripts/PlayerController.cs

    public class PlayerController : MonoBehaviour
    {
        [Header("Referens:")]
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private AnimatorPersonController _animatorPersonController;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private SlopeChecker _slopeChecker;

        [Header("Settings:")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _smoothTime = 0.2f;

        private Transform _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main.transform;
            _cinemachineCamera.Follow = _playerView.transform;
            _cinemachineCamera.LookAt = _playerView.transform;
            _cinemachineCamera.OnTargetObjectWarped(_playerView.transform, _playerView.transform.position - _cinemachineCamera.transform.position - Vector3.forward);
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            _inputReader.OnMoved += _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnMoved += _animatorPersonController.SetMove;
            _inputReader.OnJumped += _playerView.Jump;
            _inputReader.OnMoveStoped += _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnLooked += _cameraController.Rotate;
            _cameraController.OnRoteted += _playerView.Rotate;
            _cameraController.OnDirectionChanged += _playerView.SetMoveDirection;
            _groundChecker.OnGround += _playerView.SetIsGround;
            _groundChecker.OnGroundNormal += _slopeChecker.SetHit;
        }

        private void OnDisable()
        {
            _inputReader.OnMoved -= _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnMoved -= _animatorPersonController.SetMove;
            _inputReader.OnJumped -= _playerView.Jump;
            _inputReader.OnMoveStoped -= _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnLooked -= _cameraController.Rotate;
            _cameraController.OnRoteted -= _playerView.Rotate;
            _cameraController.OnDirectionChanged -= _playerView.SetMoveDirection;
            _groundChecker.OnGround -= _playerView.SetIsGround;
            _groundChecker.OnGroundNormal -= _slopeChecker.SetHit;
        }
    }
}
