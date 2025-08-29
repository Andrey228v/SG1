using UnityEngine;

namespace Assets.Scripts.Units
{
    public class SignalReader : MonoBehaviour
    {
        public bool IsMove {  get; private set; }

        public bool IsJump { get; private set; }

        private void Start()
        {
            IsMove = false;
            IsJump = false;
        }


        public void SetIsMove(bool isMove)
        {
            IsMove = isMove;
        }

        public void SetIsJump(bool isJump)
        {
            IsJump = isJump;
        }
    }
}
