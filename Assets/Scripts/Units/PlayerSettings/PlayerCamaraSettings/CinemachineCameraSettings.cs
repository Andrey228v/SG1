using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Units.PlayerSettings
{
    [CreateAssetMenu(fileName = "CinemachineCameraSettings", menuName = "ScriptableObjects/CinemachineCameraSettings")]
    public class CinemachineCameraSettings : ScriptableObject
    {
        [Header("Follow Settings")]
        [SerializeField] private Vector3 _followOffset = new Vector3(0, 2, -3);
        [SerializeField] private float _followDamping = 1f;

        [Header("Aim Settings")]
        [SerializeField] private Vector3 _aimOffset = new Vector3(0, 1, 0);
        [SerializeField] private float _aimDamping = 1f;
        [SerializeField] private CinemachineVirtualCameraBase.StandbyUpdateMode _standbyUpdate = CinemachineVirtualCameraBase.StandbyUpdateMode.Never;

        [Header("Noise Settings")]
        [SerializeField] private NoiseSettings _noiseProfile;
        [SerializeField] private float _noiseAmplitude = 0.1f;
        [SerializeField] private float _noiseFrequency = 0.1f;

        [Header("Lens Settings")]
        [SerializeField] private float _fieldOfView = 60f;
        [SerializeField] private float _orthographicSize = 5f;
        [SerializeField] private float _nearClipPlane = 0.1f;
        [SerializeField] private float _farClipPlane = 1000f;
        [SerializeField] private LensSettings.OverrideModes _lensOverride = LensSettings.OverrideModes.None;

        [Header("Camera Blending")]
        [SerializeField] private float _blendTime = 0.5f;
        [SerializeField] private CinemachineBlendDefinition.Styles _blendStyle = CinemachineBlendDefinition.Styles.EaseInOut;

        // Properties
        public Vector3 FollowOffset => _followOffset;
        public float FollowDamping => _followDamping;
        public Vector3 AimOffset => _aimOffset;
        public float AimDamping => _aimDamping;
        public CinemachineVirtualCameraBase.StandbyUpdateMode StandbyUpdate => _standbyUpdate;
        public NoiseSettings NoiseProfile => _noiseProfile;
        public float NoiseAmplitude => _noiseAmplitude;
        public float NoiseFrequency => _noiseFrequency;
        public float FieldOfView => _fieldOfView;
        public float OrthographicSize => _orthographicSize;
        public float NearClipPlane => _nearClipPlane;
        public float FarClipPlane => _farClipPlane;
        public LensSettings.OverrideModes LensOverride => _lensOverride;
        public float BlendTime => _blendTime;
        public CinemachineBlendDefinition.Styles BlendStyle => _blendStyle;
    }
}
