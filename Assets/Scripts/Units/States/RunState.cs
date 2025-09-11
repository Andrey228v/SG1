using Assets.Scripts.Units;
using Assets.Scripts.Units.States;
using UnityEngine;

namespace Assets.Scripts.StateMachineUnit
{
    public class RunState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;

        public RunState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unit.PlayerView.SetGravity(_unit.Settings.GravityGround);
            _unit.AnimatorPersonController.SetMove(true);
        }

        public void Exit()
        {

        }

        public void FixedUpdate()
        {
            
        }

        public void UpdateState()
        {
            _unit.PlayerView.Move(_unit.Settings.RunSpeed);
            CheckSwitchStates();
        }
        
        public void CheckSwitchStates()
        {
            if (_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unit.SignalReader.IsMove == false)
            {
                _unit.AnimatorPersonController.SetMove(false);
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unit.PlayerView.GetIsGrounded() == false)
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
        }
    }
}
