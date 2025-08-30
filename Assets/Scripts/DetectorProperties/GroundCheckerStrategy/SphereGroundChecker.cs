using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties.GroundCheckerStrategy
{
    [CreateAssetMenu(fileName = "SphereGroundChecker", menuName = "ScriptableObjects/SphereGroundChecker")]
    public class SphereGroundChecker : AGroundCheckerStrategy
    {
        [Header("SphereCast Settings")]
        [SerializeField] private float _sphereRadius = 0.3f;
        [SerializeField] private float _castDistance = 0.2f;
        [SerializeField] private float _groundCheckOffset = 0.05f;
        [SerializeField] private Vector3 _castOffset = new Vector3(0, 0.1f, 0);

        [Header("Debug")]
        [SerializeField] private bool _showDebugGizmos = true;
        [SerializeField] private Color _gizmoColor = Color.green;

        //[Header("Slope Handling")]
        //public float maxWalkableSlope = 45f;
        //public float slideForce = 10f;

        private bool _isGrounded;
        private RaycastHit _groundHit;
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
            Vector3 origin = unit.position + Vector3.down * _groundCheckOffset;

            _isGrounded = Physics.SphereCast(
                origin,
                _sphereRadius,
                Vector3.down,
                out _groundHit,
                _castDistance
            );

            if (_isGrounded)
            {
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

            Vector3 origin = unit.position + Vector3.down * _groundCheckOffset;
            Vector3 endPoint = origin + Vector3.down * _castDistance;

            // Draw start sphere
            Gizmos.DrawWireSphere(origin, _sphereRadius);

            // Draw cast line
            Gizmos.DrawLine(origin, endPoint);

            // Draw end sphere (hit point or max distance)
            if (_isGrounded)
            {
                Gizmos.color = Color.yellow;
                Vector3 hitPoint = origin + Vector3.down * _groundHit.distance;
                Gizmos.DrawWireSphere(hitPoint, _sphereRadius);

                // Draw ground normal
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(_groundHit.point, _groundHit.normal * 1f);
            }
            else
            {
                Gizmos.DrawWireSphere(endPoint, _sphereRadius);
            }

            // Draw connection lines between spheres
            Gizmos.color = _gizmoColor;
            DrawSphereConnectionLines(origin, _sphereRadius);
            DrawSphereConnectionLines(endPoint, _sphereRadius);
        }

        private void DrawSphereConnectionLines(Vector3 center, float radius)
        {
            // Draw cross lines to visualize sphere better
            Gizmos.DrawLine(
                center + Vector3.left * radius,
                center + Vector3.right * radius
            );
            Gizmos.DrawLine(
                center + Vector3.forward * radius,
                center + Vector3.back * radius
            );
            Gizmos.DrawLine(
                center + Vector3.up * radius,
                center + Vector3.down * radius
            );
        }



    }
}
