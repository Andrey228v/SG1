using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private bool _isJumping;
        private float _deleySetNextState = 0.2f;
        private float _currentDeley = 0.2f;
        private float _gravity = 0f;
        private float _currentGravity;
        private float _initialJumpVelocity;
        private float _currentTimeJump;

        public JumpState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
            _isJumping = false;
        }

        public void Enter()
        {
            SetupJumpVaraibles();
            _unit.PlayerView.Jump(_initialJumpVelocity);
            _unit.PlayerView.SetGravity(_gravity);
            _currentDeley = _deleySetNextState;
            _isJumping = false;
            _unit.AnimatorPersonController.SetJump(true);
        }

        public void Exit()
        {
            //_unit.PlayerView.Jump(0f);
            _unit.AnimatorPersonController.SetJump(false);
        }

        public void FixedUpdate()
        {
        }

        public void UpdateState()
        {
            CheckSwitchStates();

            //_currentGravity = _gravity - (_currentGravity - (_gravity * Time.deltaTime));
            //_unit.PlayerView.SetGravity(_currentGravity);
            //_initialJumpVelocity -= _gravity * _currentTimeJump;
            //_unit.PlayerView.Jump(_initialJumpVelocity);

            _currentTimeJump += Time.deltaTime;
            _currentDeley -= Time.deltaTime;
        }

        public void CheckSwitchStates()
        {
            if(_currentDeley < 0)
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
            }
        }

        private void SetupJumpVaraibles()
        {
            _currentTimeJump = 0f;
            //_initialJumpVelocity = 4 * _unit.Settings.MaxJumpHeight / _unit.Settings.MaxJumpTime;
            //_gravity = 8 * _unit.Settings.MaxJumpHeight / Mathf.Pow(_unit.Settings.MaxJumpTime, 2);

            float timeToApex = _unit.Settings.MaxJumpTime / 2;
            _gravity = (-2 * _unit.Settings.MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            //_gravity = _unit.Settings.Gravity;
            //_currentGravity = _gravity;
            //_initialJumpVelocity = 9.8f;
            _initialJumpVelocity = (2 * _unit.Settings.MaxJumpHeight) / timeToApex;

            //_initialJumpVelocity = Mathf.Sqrt(_unit.Settings.MaxJumpHeight * 2f * _gravity);

            //_initialJumpVelocity = _unit.Settings.MaxJumpHeight / (timeToApex - (Mathf.Pow(timeToApex,2) / 2));
        }
    }
}
