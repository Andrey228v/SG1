using Assets.Scripts.StateMachineUnit;
using UnityEngine;

namespace Assets.Scripts.Units.States
{
    public class StayState : IStateUnit
    {
        private Unit _unit;
        private PlayerStateMachine _playerStateMachine;
        private float _deleyFallTime;
        private float _currentFallTime = 0f;

        public StayState(PlayerStateMachine playerStateMachine, Unit unit) 
        {
            _currentFallTime = 0f;
            _unit = unit;
            _playerStateMachine = playerStateMachine;
            _deleyFallTime = _unit.Settings.DeleyTimeFall;
        }

        public void Enter()
        {
            _unit.PlayerView.SetGravity(_unit.Settings.GravityGround);
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
            CheckSwitchStates();
        }

        public void CheckSwitchStates()
        {
            if(_unit.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unit.SignalReader.IsMove == true)
            {
                _unit.AnimatorPersonController.SetStatic(false);
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unit.PlayerView.GetIsGrounded() == false)
            {
                _currentFallTime += Time.deltaTime;

                if (_deleyFallTime < _currentFallTime)
                {
                    _playerStateMachine.SelectState(UnitStateType.Fall);
                }
            }
        }
    }
}
