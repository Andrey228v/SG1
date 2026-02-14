using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.XInput;


public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineInputAxisController _inputAxisController;
    [SerializeField] private CinemachineOrbitalFollow _orbitalFollow;
    [field: SerializeField] public CinemachineCamera CameraCinemachine {  get; private set; }

    private Vector3 _moveDirection;
    private Vector3 _adjustedDirection;
    private Transform _cameraTransorm;
    private bool _isPause;
    private CinemachineComponentBase _cameraBody;
    
    public event Action<Vector3> OnDirectionChanged;

    public void Start()
    {
        _cameraTransorm = CameraCinemachine.transform;
        _isPause = false;
        _cameraBody = CameraCinemachine.GetCinemachineComponent(CinemachineCore.Stage.Body);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void FixedUpdate()
    {
        _adjustedDirection = Quaternion.AngleAxis(_cameraTransorm.eulerAngles.y, Vector3.up) * _moveDirection;
        OnDirectionChanged?.Invoke(_adjustedDirection);

    }

    public void MoveDirectionToCameraDirection(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
    }

    public void Pause()
    {
        _isPause = true;

        if (_cameraBody != null)
        {
            if (_inputAxisController != null)
            {
                float horizontalAxis = _orbitalFollow.HorizontalAxis.Value;

                foreach (var controller in _inputAxisController.Controllers)
                {
                    controller.Enabled = false;
                }

            }
        }
    }

    public void Continue()
    {
        _isPause = false;

        if (_cameraBody != null)
        {
            if (_inputAxisController != null)
            {
                float horizontalAxis = _orbitalFollow.HorizontalAxis.Value;

                foreach (var controller in _inputAxisController.Controllers)
                {
                    controller.Enabled = true;
                }

            }
        }
    }

    //private float GetAxisFromController(string axisName)
    //{
    //    if (_inputAxisController == null) return 0f;

    //    foreach (var controller in _inputAxisController.Controllers)
    //    {
    //        if (controller.Name == axisName)
    //        {
    //            return controller.Value;
    //        }
    //    }

    //    return 0f;
    //}
}
