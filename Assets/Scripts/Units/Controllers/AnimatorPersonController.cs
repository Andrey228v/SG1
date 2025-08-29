using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorPersonController : MonoBehaviour
    {
        private Animator _animator;

        private const string StaticIdle = "Static_b";
        private const string Speed = "Speed_f";
        private const string IsJumping = "IsJumping_b";
        private const string IsFalling_b = "IsFalling_b";

        private Vector3 _moveDirection;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetStatic(bool isStatic)
        {
            _animator.SetBool(StaticIdle, isStatic);
        }

        public void SetMove(bool isMove)
        {
            float speed;

            if (isMove)
            {
                speed = 1;
            }
            else
            {
                speed = 0;
            }

            _animator.SetFloat(Speed, speed);
        }

        public void SetJump(bool isJump) 
        {
            _animator.SetBool(IsJumping, isJump);
        }

        public void SetFall(bool isFall) 
        {
            _animator.SetBool(IsFalling_b, isFall);
        }
    }
}
