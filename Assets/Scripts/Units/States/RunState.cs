using Assets.Scripts.Units;
using Assets.Scripts.Units.States;

namespace Assets.Scripts.StateMachineUnit
{
    public class RunState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        //private float _deleySetFallState = 0.2f;
        //private float _currentDeleyFall;
        public RunState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _unit = unit;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unit.PlayerView.SetDrag(_unit.Settings.GroundDragMovement);
            _unit.AnimatorPersonController.SetMove(true);
            //_currentDeleyFall = _deleySetFallState;
        }

        public void Exit()
        {

        }

        public void FixedUpdate()
        {
            _unit.PlayerView.Move(_unit.Settings.RunSpeed, _unit.Settings.RotateSpeed);
        }

        public void UpdateState()
        {
            //_unit.PlayerView.Move(_unit.Settings.RunSpeed, _unit.Settings.RotateSpeed);
            CheckSwitchStates();
        }
        
        public void CheckSwitchStates()
        {
            if (_unit.SignalReader.IsJump == true)
            {
                _unit.PlayerView.SetDrag(0);
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unit.SignalReader.IsMove == false)
            {
                _unit.AnimatorPersonController.SetMove(false);
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unit.PlayerView.GetVelosity().y < -0.1f && _unit.PlayerView.IsGrounded == false)
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
        }
    }
}
