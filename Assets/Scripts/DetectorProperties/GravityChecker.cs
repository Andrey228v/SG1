using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityChecker : MonoBehaviour
    {
        [Header("Gravity Settings")]
        [SerializeField] private float _baseGravity = 20f;
        [SerializeField] private float _maxFallSpeed = 53.0f;
        [SerializeField] private float _gravityMultiplier = 2.0f;

        [Header("Air Control")]
        [SerializeField] private float _airControl = 0.5f;
        [SerializeField] private float _airResistance = 0.1f;
        [SerializeField]
        private AnimationCurve _airControlCurve = new AnimationCurve(
            new Keyframe(0, 0.8f),
            new Keyframe(1, 0.3f)
        );

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem _fallDustParticles;
        [SerializeField] private float _cameraShakeIntensity = 0.5f;
        [SerializeField] private float _cameraShakeDuration = 0.3f;

        private Rigidbody _rigidBody;
        private bool _isGravity;
        private bool _isFalling;
        private bool _isGrounded;
        private float _fallStartHeight;
        private Vector3 _groundNormal;
        private float _lastGroundedTime;
        private Vector3 _gravityForce;
        private float _arrowSize = 1f;
        private Color _gravityColor = Color.red;
        private Color _groundedColor = Color.green;
        private Color _fallingColor = Color.yellow;
        private float _sphereSize = 0.2f;

        private bool _isEventSent = false;

        // Public events
        public event Action<bool> OnGravityUp;
        public event Action<bool> OnGravityDown;
        public event Action OnLand;
        public event Action OnFallStart;
        public event Action<float> OnFallStarted; // Высота начала падения
        public event Action<float> OnFallImpact; // Сила удара
        public event Action<float> OnFallDamage; // Полученный урон

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.useGravity = false;
        }

        private void Update()
        {
            //CheckGravity();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
        }

        public void SetIsGround(bool isGround)
        {
            _isGrounded = isGround;
        }

        private void ApplyGravity()
        {
            if (_isGrounded == false)
            {
                _gravityForce = Vector3.down * _baseGravity;
                _rigidBody.AddForce(_gravityForce, ForceMode.Impulse);
            }
        }

        //private void StartFalling() // Это тут не нужно ....
        //{
        //    _isFalling = true;
        //    _fallStartHeight = transform.position.y;
        //    OnFallStart?.Invoke();
        //}

        private void OnDrawGizmos()
        {
            DrawGravityForce();
        }

        private void DrawGravityForce()
        {
            // Основная стрелка силы гравитации
            Vector3 startPoint = transform.position + Vector3.up * 0.5f;
            Vector3 endPoint = startPoint + _gravityForce.normalized * _arrowSize;

            Gizmos.color = _isGrounded ? _groundedColor : _fallingColor;

            // Линия гравитации
            Gizmos.DrawLine(startPoint, endPoint);

            // Стрелка на конце
            DrawArrow(endPoint, -_gravityForce.normalized, 0.3f);

            // Сфера в начале стрелки
            Gizmos.color = _gravityColor;
            Gizmos.DrawWireSphere(startPoint, _sphereSize);
        }

        private void DrawArrow(Vector3 position, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            if (direction.magnitude < 0.001f) return;

            // Рисуем основную линию
            Gizmos.DrawRay(position, direction);

            // Рисуем стрелку
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;

            Gizmos.DrawRay(position + direction, right * arrowHeadLength);
            Gizmos.DrawRay(position + direction, left * arrowHeadLength);
        }

        // Вспомогательные методы для рисования
        private void DrawCircle(Vector3 center, float radius, Color color)
        {
            Gizmos.color = color;
            int segments = 20;
            float angle = 0f;

            Vector3 lastPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            for (int i = 1; i <= segments; i++)
            {
                angle = (float)i / segments * Mathf.PI * 2;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                Gizmos.DrawLine(lastPoint, nextPoint);
                lastPoint = nextPoint;
            }
        }


        //private void HandleGravity()
        //{
        //    if (IsGrounded == false)
        //    {
        //        // Apply gravity with multiplier
        //        float effectiveGravity = _baseGravity * _gravityMultiplier;

        //        // Air resistance (increases with fall speed)
        //        float fallSpeedFactor = Mathf.Clamp01(CurrentFallSpeed / _maxFallSpeed);
        //        float resistance = _airResistance * fallSpeedFactor;

        //        _verticalVelocity -= (effectiveGravity - resistance) * Time.deltaTime;
        //        _verticalVelocity = Mathf.Max(_verticalVelocity, -_maxFallSpeed);

        //        // Update falling state
        //        if (_verticalVelocity < -0.1f && !_isFalling)
        //        {
        //            StartFalling();
        //        }
        //    }
        //    else if (_verticalVelocity < 0)
        //    {
        //        // Small downward force when grounded
        //        _verticalVelocity = -1f;
        //    }
        //}

        //private void CheckGravity()
        //{
        //    //Debug.Log($"_rigidBody.linearVelocity.y: {_rigidBody.linearVelocity.y}");

        //    if (_rigidBody.linearVelocity.y > 0)
        //    {
        //        _isGravity = true;
        //        OnGravityUp?.Invoke(_isGravity);
        //    }
        //    else if(_rigidBody.linearVelocity.y < 0)
        //    {
        //        _isGravity = true;
        //        OnGravityDown?.Invoke(_isGravity);
        //    }
        //    else if(_rigidBody.linearVelocity.y == 0)
        //    {
        //        _isGravity = false;
        //        //Debug.Log($"???????");
        //    }
        //}
    }
}
