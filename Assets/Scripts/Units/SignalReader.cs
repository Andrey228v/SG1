using UnityEngine;

namespace Assets.Scripts.Units
{
    public class SignalReader : MonoBehaviour
    {
        public bool IsMove {  get; private set; }

        public bool IsJump { get; private set; }

        public bool IsJumpButtonDownClicked {  get; private set; }

        public bool IsJumpButtonUpClicked { get; private set; }

        private bool _isDown = false;
        private bool _isUp = true;

        private void Start()
        {
            IsMove = false;
            IsJump = false;
            IsJumpButtonDownClicked = false;
        }

        public void SetIsMove(bool isMove)
        {
            IsMove = isMove;
        }

        public void SetIsJump(bool isJump)
        {
            IsJump = isJump;
        }

        public void SetIsJumpButtonDown(bool isJumpButtonDown)
        {
            IsJumpButtonDownClicked = isJumpButtonDown;
        }

        public bool GetIsJumpButtonDown()
        {
            if (IsJumpButtonDownClicked && _isDown == false)
            {
                _isDown = true;
                IsJumpButtonDownClicked = false;
            }
            else if(IsJumpButtonDownClicked == false)
            {
                _isDown = false;
            }

                return _isDown;
        }

        public void SetIsJumpButtonUp(bool isJumpButtonUp) 
        {
            IsJumpButtonUpClicked = isJumpButtonUp;
        }

        public bool GetIsJumpButtonUp()
        {
            if(IsJumpButtonUpClicked && _isUp == true)
            {
                _isUp = false;
                IsJumpButtonUpClicked = false;
            }
            else if(IsJumpButtonUpClicked == false)
            {
                _isUp = true;

            }

                return _isUp;
        }
    }
}
