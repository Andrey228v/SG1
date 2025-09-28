using System;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [field: SerializeField] public CinemachineCamera CameraCinemachine {  get; private set; }

    private Vector3 _moveDirection;
    private Vector3 _adjustedDirection;
    private Transform _cameraTransorm;
    private Transform _trackingTarget;

    public event Action<Vector3> OnDirectionChanged;

    public void Start()
    {
        _cameraTransorm = CameraCinemachine.transform;
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
}
