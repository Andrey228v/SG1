using UnityEngine;

namespace Assets.Scripts.Units.PlayerSettings.PlayerCamaraSettings
{
    [CreateAssetMenu(fileName = "FreeLookCameraSettings", menuName = "ScriptableObjects/FreeLookCameraSettings")]
    public class FreeLookCameraSettings : CinemachineCameraSettings
    {
        [Header("FreeLook Specific - Cinemachine 3")]
        [SerializeField] private float _topRigHeight = 4f;
        [SerializeField] private float _middleRigHeight = 2f;
        [SerializeField] private float _bottomRigHeight = 0.5f;
        [SerializeField] private float _rigRadius = 2f;
        [SerializeField] private float _rotationDamping = 0.5f;
        [SerializeField] private bool _commonLens = true;
        [SerializeField] private bool _enableXAxis = true;
        [SerializeField] private bool _enableYAxis = true;

        public float TopRigHeight => _topRigHeight;
        public float MiddleRigHeight => _middleRigHeight;
        public float BottomRigHeight => _bottomRigHeight;
        public float RigRadius => _rigRadius;
        public float RotationDamping => _rotationDamping;
        public bool CommonLens => _commonLens;
        public bool EnableXAxis => _enableXAxis;
        public bool EnableYAxis => _enableYAxis;
    }
}
