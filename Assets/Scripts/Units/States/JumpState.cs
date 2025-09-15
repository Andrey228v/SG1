using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private float _deleyJumpState = 0.0f;
        private float _currentDeleyJumpState = 0f;

        public JumpState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            Jump();
            _currentDeleyJumpState = 0f;
        }

        public void Exit()
        {
            _unit.PlayerView.StopJump();
            _unit.AnimatorPersonController.SetJump(false);
        }

        public void FixedUpdate()
        {
            CheckSwitchStates();
        }

        public void UpdateState()
        {
            //CheckSwitchStates();
        }

        public void CheckSwitchStates()
        {
            _currentDeleyJumpState += Time.deltaTime;

            if (_unit.SignalReader.IsMove == true && _unit.PlayerView.CharacterView.IsOnGround() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unit.SignalReader.IsMove == false && _unit.PlayerView.CharacterView.IsOnGround() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unit.PlayerView.CharacterView.IsOnGround() == false && _unit.PlayerView.GetIsFall())
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
            else if(_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                Jump();
            }
            else if(_unit.SignalReader.GetIsJumpButtonUp() && 0.0f <= _currentDeleyJumpState)
            {
                _unit.PlayerView.StopJump();
            }
        }

        private void Jump()
        {
            _unit.PlayerView.Jump();
            _unit.AnimatorPersonController.SetJump(true);
        }
    }
}
