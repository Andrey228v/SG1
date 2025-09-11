using UnityEngine;

namespace Assets.Scripts.Units
{
    public class SignalReader : MonoBehaviour
    {
        public bool IsMove {  get; private set; }

        public bool IsJump { get; private set; }

        public bool IsJumpButtonDown {  get; private set; }

        public bool IsJumpButtonUp { get; private set; }

        private bool _isDown = false;

        private void Start()
        {
            IsMove = false;
            IsJump = false;
            IsJumpButtonDown = false;
        }

        public void SetIsMove(bool isMove)
        {
            IsMove = isMove;
        }

        public void SetIsJump(bool isJump)
        {
            //Debug.Log($"isJump:{isJump}");
            IsJump = isJump;
        }

        public void SetIsJumpButtonDown(bool isJumpButtonDown)
        {

            IsJumpButtonDown = isJumpButtonDown;

            //if (isJumpButtonDown && _isDown == false)
            //{

            //    IsJumpButtonDown = true;
            //    //_isDown = true;
            //}
            ////else if (isJumpButtonDown && _isDown == true)
            ////{
            ////    IsJumpButtonDown = false;
            ////}
            //else if (isJumpButtonDown == false && _isDown == true)
            //{
            //    IsJumpButtonDown = false;
            //    //_isDown = false;
            //}
            //else if (_isDown == true)
            //{
            //    IsJumpButtonDown = false;
            //}

        }

        public bool GetIsJumpButtonDown()
        {
            Debug.Log($"TEST: {IsJumpButtonDown}, {_isDown} ");
            if (IsJumpButtonDown && _isDown == false)
            {
                _isDown = true;
                IsJumpButtonDown = false;
            }
            else if(IsJumpButtonDown == false)
            {
                _isDown = false;
            }

                return _isDown;
        }

        public void SetIsJumpButtonUp(bool isJumpButtonUp) 
        {

        }

    }
}
