using Assets.Scripts.Units.PlayerSettings.PlayerCamaraSettings;
using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [field: SerializeField] public CinemachineCamera CameraCinemachine {  get; private set; }

    [Header("Camera Settings")]
    [SerializeField] private CinemachineSettingsManager _settingsManager;
    [SerializeField] private ThirdPersonCameraSettings _thirdPersonSettings;
    [SerializeField] private FirstPersonCameraSettings _firstPersonSettings;
    [SerializeField] private FreeLookCameraSettings _freeLookSettings;

    [Header("References")]
    [SerializeField] private CinemachineCamera _virtualCamera;
    [SerializeField] private CinemachineCamera _freeLookCamera;
    //[SerializeField] private Transform _playerTransform;

    private CameraMode _currentMode = CameraMode.ThirdPerson;
    //private CinemachinePanTilt _povComponent;

    public enum CameraMode
    {
        ThirdPerson,
        FirstPerson,
        FreeLook
    }

    private Vector3 _moveDirection;
    private Vector3 _adjustedDirection;
    private Transform _cameraTransorm;

    public event Action<Vector3> OnRoteted;
    public event Action<Vector3> OnDirectionChanged;

    private void Awake()
    {
        _cameraTransorm = CameraCinemachine.transform;
        Cursor.lockState = CursorLockMode.Locked;
        //CameraCinemachine.PositionControll
    }

    private void Start()
    {
        //InitializeCamera();
        //SwitchToThirdPerson();
    }

    private void InitializeCamera()
    {
        //if (_virtualCamera == null)
        //    _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        //if (_freeLookCamera == null)
        //    _freeLookCamera = FindObjectOfType<CinemachineFreeLook>();

        //if (_settingsManager == null)
        //    _settingsManager = GetComponent<CinemachineSettingsManager>();

        //// Get POV component for first person
        //if (_virtualCamera != null)
        //{
        //    _povComponent = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        //}
    }

    private void Update()
    {
        _adjustedDirection = Quaternion.AngleAxis(_cameraTransorm.eulerAngles.y, Vector3.up) * _moveDirection;
        OnDirectionChanged?.Invoke(_adjustedDirection);
        //HandleCameraInput();
    }

    public void MoveDirectionToCameraDirection(Vector2 direction)
    {
        _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
    }

    private void HandleCameraInput()
    {
        // Camera mode switching
        //if (Input.GetKeyDown(KeyCode.F1)) SwitchToThirdPerson();
        //if (Input.GetKeyDown(KeyCode.F2)) SwitchToFirstPerson();
        //if (Input.GetKeyDown(KeyCode.F3)) SwitchToFreeLook();

        // Camera rotation for first person
        //if (_currentMode == CameraMode.FirstPerson && _povComponent != null)
        //{
        //    HandleFirstPersonRotation();
        //}
    }

    public void SwitchToThirdPerson()
    {
        _currentMode = CameraMode.ThirdPerson;
        EnableCamera(_virtualCamera, true);
        EnableCamera(_freeLookCamera, false);
        _settingsManager.ApplyThirdPersonSettings(_thirdPersonSettings);
    }

    public void SwitchToFirstPerson()
    {
        _currentMode = CameraMode.FirstPerson;
        EnableCamera(_virtualCamera, true);
        EnableCamera(_freeLookCamera, false);
        _settingsManager.ApplyFirstPersonSettings(_firstPersonSettings);

        //if (_povComponent == null && _virtualCamera != null)
        //{
        //    _povComponent = _virtualCamera.AddCinemachineComponent<CinemachinePOV>();
        //}
    }

    public void SwitchToFreeLook()
    {
        _currentMode = CameraMode.FreeLook;
        EnableCamera(_virtualCamera, false);
        EnableCamera(_freeLookCamera, true);
        _settingsManager.ApplySettings(_freeLookSettings);
    }

    private void EnableCamera(CinemachineVirtualCameraBase camera, bool enable)
    {
        if (camera != null)
        {
            camera.gameObject.SetActive(enable);
            camera.Priority = enable ? 100 : 0;
        }
    }

    //private void HandleCameraEffects()
    //{
    //    // Handle camera shake based on player movement
    //    if (_playerTransform != null)
    //    {
    //        Rigidbody rb = _playerTransform.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            float playerSpeed = rb.velocity.magnitude;
    //            float shakeIntensity = Mathf.Clamp01(playerSpeed / 10f) * 0.2f;
    //            _settingsManager.SetNoiseIntensity(shakeIntensity, shakeIntensity * 2f);
    //        }
    //    }

    //    // Handle head bob for first person
    //    if (_currentMode == CameraMode.FirstPerson && _firstPersonSettings != null && _firstPersonSettings.EnableHeadBob)
    //    {
    //        HandleHeadBob();
    //    }
    //}

    private void HandleHeadBob()
    {
        if (_virtualCamera != null)
        {
            float bob = Mathf.Sin(Time.time * _firstPersonSettings.HeadBobFrequency) * _firstPersonSettings.HeadBobAmplitude;
            Vector3 offset = _firstPersonSettings.FirstPersonOffset;
            offset.y += bob;

            _settingsManager.SetFollowOffset(offset);
        }
    }

    public void ApplyCameraShake(float intensity, float duration = 0.3f)
    {
        _settingsManager.EnableCameraShake(true);

        CancelInvoke(nameof(ResetShake));
        Invoke(nameof(ResetShake), duration);
    }

    private void ResetShake()
    {
        _settingsManager.EnableCameraShake(false);
    }

    public void SetCameraSensitivity(float sensitivity)
    {
        if (_firstPersonSettings != null)
        {
            var modifiedSettings = ScriptableObject.CreateInstance<FirstPersonCameraSettings>();
            modifiedSettings.name = "ModifiedSensitivitySettings";
            //modifiedSettings.MouseSensitivity = sensitivity;
            _settingsManager.ApplyFirstPersonSettings(modifiedSettings);
        }
    }

    public void SetFOV(float fov, float transitionTime = 0.5f)
    {
        _settingsManager.SetFieldOfView(fov, transitionTime);
    }


}
