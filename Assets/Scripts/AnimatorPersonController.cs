using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorPersonController : MonoBehaviour
    {
        private Animator _animator;

        private const string StaticIdle = "Static_b";
        private const string Speed = "Speed_f";
        private const string IsJumping = "IsJumping_b";

        private Vector3 _moveDirection;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetStatic(bool isStatic)
        {
            _animator.SetBool(StaticIdle, isStatic);
        }

        public void SetMove(Vector2 direction)
        {
            _moveDirection = new Vector3(direction.x, 0, direction.y);
            float speed = _moveDirection.magnitude;
            //Debug.Log($"speed: {speed}, _moveDirection: {_moveDirection}, direction: {direction}");
            _animator.SetFloat(Speed, speed);
        }

    }
}
