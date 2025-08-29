using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GroundChecker : MonoBehaviour
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

        public event Action<bool> OnGround;
        public event Action<bool, RaycastHit> OnGroundNormal;

        private void Update()
        {
            CheckGround();
        }

        public Vector3 GetGroundNormal()
        {
            return _isGrounded ? _groundHit.normal : Vector3.up;
        }

        public float GetGroundDistance()
        {
            return _isGrounded ? _groundHit.distance : Mathf.Infinity;
        }

        private void CheckGround()
        {
            Vector3 boxCenter = transform.position + Vector3.down * _groundCheckOffset;
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
                _lastGroundTime = Time.time;
                OnGroundNormal?.Invoke(_isGrounded, _groundHit);
            }
            
            if(_isGrounded && _isEventSent == false)
            {
                OnGround?.Invoke(_isGrounded);
                _isEventSent = true;
            }
            else if(_isGrounded == false && _isEventSent)
            {
                OnGround?.Invoke(_isGrounded);
                _isEventSent = false;
            }


        }

        private void OnDrawGizmos()
        {
            if (!_showDebugGizmos) return;

            Gizmos.color = _isGrounded ? _gizmoColor : Color.red;

            Vector3 boxCenter = transform.position + Vector3.down * _groundCheckOffset;
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
    }
}
