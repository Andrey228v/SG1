using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private bool _isJumping;
        private float _gravity = 0f;
        private float _currentGravity;
        private float _initialJumpVelocity;
        private float _currentTimeJump;

        public JumpState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
            
        }

        public void Enter()
        {
            SetupJumpVaraibles();
            _unit.PlayerView.Jump(_initialJumpVelocity);
            _unit.PlayerView.SetGravity(_gravity);
            _unit.AnimatorPersonController.SetJump(true);
            _isJumping = true;
        }

        public void Exit()
        {
            _unit.AnimatorPersonController.SetJump(false);
        }

        public void FixedUpdate()
        {
        }

        public void UpdateState()
        {
            CheckSwitchStates();

            _currentTimeJump += Time.deltaTime;
        }

        public void CheckSwitchStates()
        {
            if (_unit.SignalReader.IsMove == true && _unit.PlayerView.GetIsGrounded())
            {
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unit.SignalReader.IsMove == false && _unit.PlayerView.GetIsGrounded())
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if(_unit.PlayerView.GetIsGrounded() == false && _unit.PlayerView.GetIsFall())
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
            else if (_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                JumpDouble();
            }
        }

        private void JumpDouble()
        {
            SetupJumpVaraibles();
            _unit.PlayerView.Jump(_initialJumpVelocity);
            _unit.PlayerView.SetGravity(_gravity);
            _unit.AnimatorPersonController.SetJump(true);
            _isJumping = false;
        }

        private void SetupJumpVaraibles()
        {
            _currentTimeJump = 0f;
            float timeToApex = _unit.Settings.MaxJumpTime / 2;
            _gravity = (-2 * _unit.Settings.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * _unit.Settings.MaxJumpHeight) / timeToApex;
        }
    }
}
