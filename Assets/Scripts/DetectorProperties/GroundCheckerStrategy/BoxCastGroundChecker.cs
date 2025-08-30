using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties.GroundCheckerStrategy
{
    [CreateAssetMenu(fileName = "BoxCastGroundChecker", menuName = "ScriptableObjects/BoxCastGroundChecker")]
    public class BoxCastGroundChecker : AGroundCheckerStrategy
    {
        [Header("BoxCast Settings")]
        [SerializeField] private Vector3 _boxSize = new Vector3(0.5f, 0.1f, 0.5f);
        [SerializeField] private float _castDistance = 0.2f;
        //[SerializeField] private float _groundCheckDistance = 0.1f;
        [SerializeField] private float _groundCheckOffset = 0.05f;

        [Header("Debug")]
        [SerializeField] private bool _showDebugGizmos = true;
        [SerializeField] private Color _gizmoColor = Color.green;

        private bool _isGrounded;
        private RaycastHit _groundHit;
        private float _lastGroundTime;
        private bool _isEventSent = false;

        public override event Action<bool> OnGround;
        public override event Action<bool, RaycastHit> OnGroundNormal;

        public override Vector3 GetGroundNormal()
        {
            return _isGrounded ? _groundHit.normal : Vector3.up;
        }

        public override float GetGroundDistance()
        {
            return _isGrounded ? _groundHit.distance : Mathf.Infinity;
        }

        public override void CheckGround(Transform unit)
        {
            Vector3 boxCenter = unit.position + Vector3.down * _groundCheckOffset;
            //Vector3 boxHalfExtents = _boxSize * 0.5f;

            _isGrounded = Physics.BoxCast
                (
                    boxCenter,
                    _boxSize * 0.5f,
                    Vector3.down,
                    out _groundHit,
                    Quaternion.identity,
                    _castDistance
                );

            if (_isGrounded)
            {
                //Debug.DrawRay(unit.position, _groundHit.);
                DrawRaycastHit(_groundHit);
                _lastGroundTime = Time.time;
                OnGroundNormal?.Invoke(_isGrounded, _groundHit);
            }

            if (_isGrounded && _isEventSent == false)
            {
                OnGround?.Invoke(_isGrounded);
                _isEventSent = true;
            }
            else if (_isGrounded == false && _isEventSent)
            {
                OnGround?.Invoke(_isGrounded);
                _isEventSent = false;
            }
        }

        public override void OnDrawGizmos(Transform unit)
        {
            if (!_showDebugGizmos) return;

            Gizmos.color = _isGrounded ? _gizmoColor : Color.red;

            Vector3 boxCenter = unit.position + Vector3.down * _groundCheckOffset;
            Vector3 boxBottomCenter = boxCenter + Vector3.down * _castDistance;

            Gizmos.DrawWireCube(boxCenter, _boxSize);
            Gizmos.DrawWireCube(boxBottomCenter, _boxSize);

            Vector3 halfSize = _boxSize * 0.5f;

            Vector3[] corners = new Vector3[]
            {
                new Vector3(halfSize.x, halfSize.y, halfSize.z),
                new Vector3(-halfSize.x, halfSize.y, halfSize.z),
                new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                new Vector3(-halfSize.x, -halfSize.y, -halfSize.z)
            };

            foreach (Vector3 corner in corners)
            {
                Gizmos.DrawLine(boxCenter + corner, boxBottomCenter + corner);
            }
        }

        private void DrawRaycastHit(RaycastHit hit)
        {
            if (hit.collider != null)
            {
                // Отрисовываем луч от начала до точки попадания
                Debug.DrawRay(hit.point - hit.normal * 0.1f, hit.normal * 0.2f, Color.red, 0.1f);

                // Отрисовываем нормаль поверхности
                Debug.DrawRay(hit.point, hit.normal * 1f, Color.blue, 0.1f);

                // Отрисовываем точку попадания
                Debug.DrawLine(hit.point - Vector3.right * 0.1f, hit.point + Vector3.right * 0.1f, Color.green, 0.1f);
                Debug.DrawLine(hit.point - Vector3.forward * 0.1f, hit.point + Vector3.forward * 0.1f, Color.green, 0.1f);
            }
        }
    }
}
