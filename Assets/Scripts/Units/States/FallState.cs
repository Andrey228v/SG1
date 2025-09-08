using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class FallState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private float _gravity;
        private float _currentGravity;

        public FallState(PlayerStateMachine playerStateMachine, Unit unit)
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }


        public void Enter()
        {
            //_unit.PlayerView.SetDrag(_unit.Settings.DragFall);
            //_unit.PlayerView.SetGravity(_unit.Settings.Gravity);
            _gravity = _unit.Settings.Gravity;
            _currentGravity = _gravity;

            _unit.AnimatorPersonController.SetFall(true);
        }

        public void Exit()
        {
            //_unit.PlayerView.Jump(0f);
            _unit.AnimatorPersonController.SetFall(false);
        }

        public void FixedUpdate()
        {
            //_unit.PlayerView.Move(_unit.Settings.JumpSpeedMove, _unit.Settings.RotateSpeed);
        }

        public void UpdateState()
        {
            //_currentGravity = _gravity - (_currentGravity - (_gravity * Time.deltaTime));
            _currentGravity += _gravity;
            _unit.PlayerView.SetGravity(_currentGravity);

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
