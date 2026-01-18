using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class StayState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;

        public StayState(PlayerStateMachine playerStateMachine, Unit unit)
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unit.AnimatorPersonController.SetStatic(true);
        }

        public void Exit()
        {

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
            if (_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unit.SignalReader.IsMove == true)
            {
                _unit.AnimatorPersonController.SetStatic(false);
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unit.PlayerView.CharacterView.IsFalling())
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
        }
    }
}
