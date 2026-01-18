using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class FallState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;

        public FallState(PlayerStateMachine playerStateMachine, Unit unit)
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unit.AnimatorPersonController.SetFall(true);
        }

        public void Exit()
        {
            _unit.AnimatorPersonController.SetFall(false);
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
            if (_unit.PlayerView.CharacterView.IsOnGround())
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
        }
    }
}
