using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties
{
    public class GravityChecker : MonoBehaviour
    {

        private bool _isGrounded;


        public void SetIsGround(bool isGround)
        {
            _isGrounded = isGround;
        }

        private void ApplyGravity()
        {
            if (_isGrounded == false)
            {
                //_gravityForce = Vector3.down * _baseGravity;
                //_rigidBody.AddForce(_gravityForce, ForceMode.Impulse);


            }
        }
    }
}
