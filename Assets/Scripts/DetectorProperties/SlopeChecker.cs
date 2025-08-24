using UnityEngine;

namespace Assets.Scripts.DetectorProperties
{
    public class SlopeChecker : MonoBehaviour
    {
        [SerializeField] private float _maxSlopeAngle = 75f;
        
        private bool _isGrounded;
        private RaycastHit _groundHit;


        public void SetHit(bool isGrounded, RaycastHit _hit)
        {
            _isGrounded = isGrounded;
            _groundHit = _hit;
        }

        public float GetGroundSlopeAngle()
        {
            return _isGrounded ? Vector3.Angle(_groundHit.normal, Vector3.up) : 0f;
        }

        public bool IsWalkableSlope()
        {
            return _isGrounded && GetGroundSlopeAngle() <= _maxSlopeAngle;
        }

        private void OnDrawGizmos()
        {
            if (_isGrounded)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(_groundHit.point, _groundHit.normal);
                Gizmos.DrawSphere(_groundHit.point, 0.05f);
            }
        }
    }
}
