using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [field: SerializeField] public CinemachineCamera CameraCinemachine {  get; private set; }

    private Vector3 _moveDirection;
    private Vector3 _adjustedDirection;
    private Transform _cameraTransorm;

    public event Action<Vector3> OnRoteted;
    public event Action<Vector3> OnDirectionChanged;

    private void Awake()
    {
        _cameraTransorm = CameraCinemachine.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _adjustedDirection = Quaternion.AngleAxis(_cameraTransorm.eulerAngles.y, Vector3.up) * _moveDirection;
        OnDirectionChanged?.Invoke(_adjustedDirection);
    }

    public void Rotate(Vector2 direction, bool isMouseUse)
    {

    }

    public void MoveDirectionToCameraDirection(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
    }
}
