using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private bool _isJumping;
        private float _deleySetNextState = 0.1f;
        private float _currentDeley = 0.1f;

        public JumpState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
            _isJumping = false;
        }

        public void Enter()
        {
            _currentDeley = _deleySetNextState;
            _isJumping = false;
            _unit.AnimatorPersonController.SetJump(true);
        }

        public void Exit()
        {
            _unit.AnimatorPersonController.SetJump(false);
        }

        public void FixedUpdate()
        {
            if (_isJumping == false) 
            {
                _unit.PlayerView.Jump(_playerStateMachine.Settings.JumpForce);
                _isJumping = true;
            }

            
        }

        public void UpdateState()
        {
            CheckSwitchStates();
            _currentDeley -= Time.deltaTime;
        }

        public void CheckSwitchStates()
        {
            if(_currentDeley < 0)
            {
                if (_unit.SignalReader.IsMove == true && _unit.PlayerView.IsGrounded)
                {
                    _playerStateMachine.SelectState(UnitStateType.Run);
                }
                else if (_unit.SignalReader.IsMove == false && _unit.PlayerView.IsGrounded)
                {
                    _playerStateMachine.SelectState(UnitStateType.Stay);
                }
                else if(_unit.PlayerView.GetVelosity().y < 0 && _unit.PlayerView.IsGrounded == false)
                {
                    _playerStateMachine.SelectState(UnitStateType.Fall);
                }
            }
            
            
        }


    }
}
