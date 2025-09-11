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
        }

        public void UpdateState()
        {
            CheckSwitchStates();
        }

        public void CheckSwitchStates()
        {
            if (_unit.PlayerView.GetIsGrounded() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
        }
    }
}
