using UnityEngine;

namespace Assets.Scripts.Units.PlayerSettings.PlayerCamaraSettings
{
    [CreateAssetMenu(fileName = "ThirdPersonCameraSettings", menuName = "ScriptableObjects/ThirdPersonCameraSettings")]
    public class ThirdPersonCameraSettings : CinemachineCameraSettings
    {
        [Header("Third Person Specific")]
        [SerializeField] private float _shoulderOffset = 0.5f;
        [SerializeField] private float _cameraDistance = 3f;
        [SerializeField] private float _cameraHeight = 1.5f;
        [SerializeField] private float _rotationSpeed = 180f;
        [SerializeField] private bool _enableCollisionDetection = true;
        [SerializeField] private LayerMask _collisionMask = 1;
        [SerializeField] private float _collisionRadius = 0.2f;

        public float ShoulderOffset => _shoulderOffset;
        public float CameraDistance => _cameraDistance;
        public float CameraHeight => _cameraHeight;
        public float RotationSpeed => _rotationSpeed;
        public bool EnableCollisionDetection => _enableCollisionDetection;
        public LayerMask CollisionMask => _collisionMask;
        public float CollisionRadius => _collisionRadius;
    }
}
