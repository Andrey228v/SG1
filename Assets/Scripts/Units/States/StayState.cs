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
            _unit.PlayerView.SetDrag(_unit.Settings.GroundDragStay);
            _unit.AnimatorPersonController.SetStatic(true);
        }

        public void Exit()
        {
            
        }

        public void FixedUpdate()
        {
           
        }

        public void UpdateState()
        {
            //_unit.PlayerView.CheckDrag(_playerStateMachine.Settings.GroundDragStay, _playerStateMachine.Settings.GroundDragMovement);
            //_unit.PlayerView.SetDrag(_playerStateMachine.Settings.GroundDragStay);
            CheckSwitchStates();
        }

        public void CheckSwitchStates()
        {
            if(_unit.SignalReader.IsJump == true)
            {
                //_unit.AnimatorPersonController.SetJump(true);
                //_unit.PlayerView.SetMoveDirection(Vector3.zero);
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unit.SignalReader.IsMove == true)
            {
                _unit.AnimatorPersonController.SetStatic(false);
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unit.PlayerView.GetVelosity().y < -0.1f && _unit.PlayerView.IsGrounded == false)
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
        }
    }
}
