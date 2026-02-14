using Assets.Scripts.Utilites;
using ECM2;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorPersonController : IPause
    {
        private Animator _animator;

        private const string StaticIdle = "Static_b";
        private const string Speed = "Speed_f";
        private const string IsJumping = "IsJumping_b";
        private const string IsFalling_b = "IsFalling_b";

        private Vector3 _moveDirection;
        private float _animationCurrentSpeed;

        public AnimatorPersonController(Character character)
        {
            _animator = character.animator;
        }

        public void SetStatic(bool isStatic)
        {
            _animator.SetBool(StaticIdle, isStatic);
            _animationCurrentSpeed = _animator.speed;
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
            _animationCurrentSpeed = _animator.speed;
        }

        public void SetJump(bool isJump)
        {
            _animator.SetBool(IsJumping, isJump);
            _animationCurrentSpeed = _animator.speed;
        }

        public void SetFall(bool isFall)
        {
            _animator.SetBool(IsFalling_b, isFall);
            _animationCurrentSpeed = _animator.speed;
        }

        public void Continue()
        {
            _animator.speed = _animationCurrentSpeed;
        }

        public void Pause()
        {
            _animator.speed = 0;
        }
    }
}
