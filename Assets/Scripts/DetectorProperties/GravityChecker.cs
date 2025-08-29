using System;
using UnityEngine;

namespace Assets.Scripts.DetectorProperties
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityChecker : MonoBehaviour
    {
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
