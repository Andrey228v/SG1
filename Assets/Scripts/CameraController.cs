using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineCamera _cameraCinemachine;

    //[SerializeField] private Transform _mainCamera;

    //private Camera _camera;
    private float _angleX;
    private float _angleY;
    private float _angleZ;
    private Vector3 _lastPosition;
    private Vector3 _moveDirection;
    private Vector3 _adjustedDirection;
    private Camera _mainCamera;
    private Transform _cameraTransorm;

    public event Action<Vector3> OnRoteted;
    public event Action<Quaternion> OnRotetedQuaternion;
    public event Action<Vector3> OnDirectionChanged;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraTransorm = _cameraCinemachine.transform;

        Cursor.lockState = CursorLockMode.Locked;

        //_angleX = _cameraTransorm.rotation.eulerAngles.x;
        //_angleY = _cameraTransorm.rotation.eulerAngles.y;
        //_angleZ = _cameraTransorm.rotation.eulerAngles.z;

        _lastPosition = transform.forward;
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
