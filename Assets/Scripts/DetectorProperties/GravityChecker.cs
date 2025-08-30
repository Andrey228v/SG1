using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityChecker : MonoBehaviour
    {
        [Header("Gravity Settings")]
        [SerializeField] private float _baseGravity = 9.81f;
        [SerializeField] private float _maxFallSpeed = 53.0f;
        [SerializeField] private float _gravityMultiplier = 2.0f;

        [Header("Air Resistance")]
        [SerializeField] private float _airDrag = 0.1f;
        [SerializeField] private float _terminalVelocity = 60.0f;
        [SerializeField]
        private AnimationCurve _airResistanceCurve = new AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(1, 1)
        );

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem _fallDustParticles;
        [SerializeField] private float _cameraShakeIntensity = 0.5f;
        [SerializeField] private float _cameraShakeDuration = 0.3f;

        // Public events
        public event Action<float> OnFallStarted; // Высота начала падения
        public event Action<float> OnFallImpact; // Сила удара
        public event Action<float> OnFallDamage; // Полученный урон

        private Rigidbody _rigidBody;
        private bool _isGravity;
        private bool _isEventSent = false;

        public event Action<bool> OnGravityUp;
        public event Action<bool> OnGravityDown;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CheckGravity();
        }

        private void CheckGravity()
        {
            //Debug.Log($"_rigidBody.linearVelocity.y: {_rigidBody.linearVelocity.y}");

            if (_rigidBody.linearVelocity.y > 0)
            {
                _isGravity = true;
                OnGravityUp?.Invoke(_isGravity);
            }
            else if(_rigidBody.linearVelocity.y < 0)
            {
                _isGravity = true;
                OnGravityDown?.Invoke(_isGravity);
            }
            else if(_rigidBody.linearVelocity.y == 0)
            {
                _isGravity = false;
                //Debug.Log($"???????");
            }
        }
    }
}
