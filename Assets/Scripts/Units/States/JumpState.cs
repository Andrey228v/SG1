using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;

        public JumpState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            Jump();
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
            
        }

        public void CheckSwitchStates()
        {
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
            else if(_unit.SignalReader.GetIsJumpButtonUp())
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
