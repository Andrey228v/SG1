using Assets.Scripts.Units.PlayerSettings;
using Assets.Scripts.Units.PlayerSettings.PlayerCamaraSettings;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CinemachineSettingsManager : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] private CinemachineCamera _virtualCamera;
    [SerializeField] private CinemachineCamera _freeLookCamera;

    [Header("Settings")]
    [SerializeField] private CinemachineCameraSettings _currentSettings;
    [SerializeField] private CinemachineCameraSettings _defaultSettings;

    private CinemachineFollow _transposer;
    private CinemachineRotationComposer _composer;
    private CinemachineBasicMultiChannelPerlin _noise;
    private CinemachineOrbitalFollow _orbitalFollow;

    public CinemachineCameraSettings CurrentSettings => _currentSettings;

    private void Awake()
    {
        InitializeCameraComponents();
        ApplySettings(_currentSettings);
    }

    private void InitializeCameraComponents()
    {
        if (_virtualCamera != null)
        {
            _transposer = _virtualCamera.GetComponent<CinemachineFollow>();
            _composer = _virtualCamera.GetComponent<CinemachineRotationComposer>();
            _noise = _virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            _orbitalFollow = _freeLookCamera.GetComponent<CinemachineOrbitalFollow>();
        }
    }

    public void ApplySettings(CinemachineCameraSettings settings)
    {
        if (settings == null) return;

        _currentSettings = settings;
        ApplySettingsToCamera(settings);
    }

    private void ApplySettingsToCamera(CinemachineCameraSettings settings)
    {
        if (_virtualCamera != null)
        {
            ApplyToVirtualCamera(settings);
        }

        if (_orbitalFollow != null)
        {
            ApplyToFreeLookCamera(settings);
        }
    }

    private void ApplyToVirtualCamera(CinemachineCameraSettings settings)
    {
        // Follow settings for Transposer
        if (_transposer != null)
        {
            _transposer.FollowOffset = settings.FollowOffset;
            
            //_transposer. = settings.FollowDamping;
            //_transposer.m_YDamping = settings.FollowDamping;
            //_transposer.m_ZDamping = settings.FollowDamping;
        }

        // Aim settings for Composer
        if (_composer != null)
        {
            //_composer.m_TrackedObjectOffset = settings.AimOffset;
            //_composer.m_HorizontalDamping = settings.AimDamping;
            //_composer.m_VerticalDamping = settings.AimDamping;
        }

        // Ensure noise component exists
        if (_noise == null && _virtualCamera != null)
        {
            _noise = _virtualCamera.AddComponent<CinemachineBasicMultiChannelPerlin>();
        }

        // Noise settings
        if (_noise != null)
        {
            _noise.NoiseProfile = settings.NoiseProfile;
            _noise.AmplitudeGain = settings.NoiseAmplitude;
            _noise.FrequencyGain = settings.NoiseFrequency;
        }

        // Lens settings
        LensSettings lens = _virtualCamera.Lens;
        lens.FieldOfView = settings.FieldOfView;
        lens.OrthographicSize = settings.OrthographicSize;
        lens.NearClipPlane = settings.NearClipPlane;
        lens.FarClipPlane = settings.FarClipPlane;
        lens.ModeOverride = settings.LensOverride;
        _virtualCamera.Lens = lens;

        // Standby update mode
        _virtualCamera.StandbyUpdate = settings.StandbyUpdate;
    }

    private void ApplyToFreeLookCamera(CinemachineCameraSettings settings)
    {
        if (_orbitalFollow == null) return;

        if (settings is FreeLookCameraSettings freeLookSettings)
        {
            //_orbitalFollow.Orbits[0].Height = freeLookSettings.TopRigHeight;
            //_orbitalFollow.m_Orbits[1].m_Height = freeLookSettings.MiddleRigHeight;
            //_orbitalFollow.m_Orbits[2].m_Height = freeLookSettings.BottomRigHeight;
            //_orbitalFollow.m_Orbits[0].m_Radius = freeLookSettings.RigRadius;
            //_orbitalFollow.m_Orbits[1].m_Radius = freeLookSettings.RigRadius;
            //_orbitalFollow.m_Orbits[2].m_Radius = freeLookSettings.RigRadius;

            //_orbitalFollow.m_CommonLens = freeLookSettings.CommonLens;
            //_orbitalFollow.m_XAxis.m_MaxSpeed = freeLookSettings.EnableXAxis ? freeLookSettings.RotationDamping : 0;
            //_orbitalFollow.m_YAxis.m_MaxSpeed = freeLookSettings.EnableYAxis ? freeLookSettings.RotationDamping : 0;
        }

        //LensSettings lens = _orbitalFollow.Lens;
        //lens.FieldOfView = settings.FieldOfView;
        //lens.OrthographicSize = settings.OrthographicSize;
        //_orbitalFollow.m_Lens = lens;
    }

    public void ApplyThirdPersonSettings(ThirdPersonCameraSettings settings)
    {
        if (settings == null) return;

        ApplySettings(settings);

        if (_virtualCamera != null && _transposer != null)
        {
            var collision = _virtualCamera.GetComponent<CinemachineDecollider>();
            if (settings.EnableCollisionDetection)
            {
                if (collision == null)
                {
                    collision = _virtualCamera.AddComponent<CinemachineDecollider>();
                }
                //collision.m_MinimumDistanceFromTarget = settings.CollisionRadius;
                //collision.m_CollideAgainst = settings.CollisionMask;
            }
            else if (collision != null)
            {
                Destroy(collision);
            }
        }
    }

    public void ApplyFirstPersonSettings(FirstPersonCameraSettings settings)
    {
        if (settings == null) return;

        ApplySettings(settings);

        // Additional first person setup for Cinemachine 3
        if (_virtualCamera != null && _transposer != null)
        {
            _transposer.FollowOffset = settings.FirstPersonOffset;

            // Setup for first person view
            var pov = _virtualCamera.GetComponent<CinemachinePOV>();
            if (pov == null)
            {
                pov = _virtualCamera.AddComponent<CinemachinePOV>();
            }
            pov.m_VerticalAxis.m_MaxValue = settings.VerticalClampAngle;
            pov.m_VerticalAxis.m_MinValue = -settings.VerticalClampAngle;
        }
    }

    public void ResetToDefault()
    {
        ApplySettings(_defaultSettings);
    }

    // Runtime modifications for Cinemachine 3
    public void SetFieldOfView(float fov, float transitionTime = 0.5f)
    {
        if (_virtualCamera != null)
        {
            LensSettings lens = _virtualCamera.Lens;
            lens.FieldOfView = fov;
            _virtualCamera.Lens = lens;
        }

        if (_freeLookCamera != null)
        {
            LensSettings lens = _freeLookCamera.Lens;
            lens.FieldOfView = fov;
            _freeLookCamera.Lens = lens;
        }
    }

    public void SetNoiseIntensity(float amplitude, float frequency)
    {
        if (_noise != null)
        {
            _noise.AmplitudeGain = amplitude;
            _noise.FrequencyGain = frequency;
        }
    }

    public void EnableCameraShake(bool enable, float transitionTime = 0.3f)
    {
        if (_noise != null)
        {
            float targetAmplitude = enable ? _currentSettings.NoiseAmplitude : 0f;
            float targetFrequency = enable ? _currentSettings.NoiseFrequency : 0f;

            //LeanTween.value(gameObject, _noise.m_AmplitudeGain, targetAmplitude, transitionTime)
            //    .setOnUpdate(amp => _noise.m_AmplitudeGain = amp);

            //LeanTween.value(gameObject, _noise.m_FrequencyGain, targetFrequency, transitionTime)
            //    .setOnUpdate(freq => _noise.m_FrequencyGain = freq);
        }
    }

    public void SetFollowOffset(Vector3 offset, float transitionTime = 0.5f)
    {
        if (_transposer != null)
        {
            //LeanTween.value(gameObject, _transposer.m_FollowOffset, offset, transitionTime)
            //    .setOnUpdate(newOffset => _transposer.m_FollowOffset = newOffset);
        }
    }
}