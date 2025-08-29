using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
    //https://github.com/adammyhre/3D-Platformer/blob/master/Assets/_Project/Scripts/PlayerController.cs

    public class PlayerController : MonoBehaviour
    {
        [Header("Referens:")]
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Unit _unit;

        private void Awake()
        {
            _unit.Awake();
            _cameraController.CameraCinemachine.Follow = _unit.PlayerView.transform;
            _cameraController.CameraCinemachine.LookAt = _unit.PlayerView.transform;
            _cameraController.CameraCinemachine.OnTargetObjectWarped(_unit.PlayerView.transform, _unit.PlayerView.transform.position - _cameraController.CameraCinemachine.transform.position - Vector3.forward);
        }

        private void OnEnable()
        {
            _inputReader.OnDirectionMoveChandged += _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnLooked += _cameraController.Rotate;
            _cameraController.OnDirectionChanged += _unit.ProcessSignalDirection;
            _inputReader.OnMoved += _unit.SetProcessSignalMove;
            _inputReader.OnStoped += _unit.SetProcessSignalStop;
            _inputReader.OnJumped += _unit.ProcessSignalJump;
            _inputReader.OnJumpedCanceled += _unit.ProcessSignalJumpStope;

        }

        private void OnDisable()
        {
            _inputReader.OnDirectionMoveChandged -= _cameraController.MoveDirectionToCameraDirection;
            _inputReader.OnLooked -= _cameraController.Rotate;
            _cameraController.OnDirectionChanged -= _unit.ProcessSignalDirection;
            _inputReader.OnMoved -= _unit.SetProcessSignalMove;
            _inputReader.OnStoped -= _unit.SetProcessSignalStop;
            _inputReader.OnJumped -= _unit.ProcessSignalJump;
            _inputReader.OnJumpedCanceled -= _unit.ProcessSignalJumpStope;

        }
    }
}
