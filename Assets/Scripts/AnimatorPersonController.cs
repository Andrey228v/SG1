using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorPersonController : MonoBehaviour
    {
        private Animator _animator;

        private const string Speed = "Speed_f";

        private Vector3 _moveDirection;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMove(Vector2 direction)
        {
            _moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
            float speed = _moveDirection.magnitude;
            Debug.Log(speed);
            _animator.SetFloat(Speed, speed);
        }

    }
}
