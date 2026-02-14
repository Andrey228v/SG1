using Assets.Scripts.StateMachineUnit;

namespace Assets.Scripts.Units.States
{
    public class StayState : IStateUnit
    {
        private UnitSignalReader _unitSignalReader;
        private UnitStateMachine _playerStateMachine;

        public StayState(UnitStateMachine playerStateMachine, UnitSignalReader unitSignalReader)
        {
            _unitSignalReader = unitSignalReader;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unitSignalReader.AnimatorPersonController.SetStatic(true);
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
            if (_unitSignalReader.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
            else if (_unitSignalReader.SignalReader.IsMove == true)
            {
                _unitSignalReader.AnimatorPersonController.SetStatic(false);
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unitSignalReader.PlayerView.CharacterView.IsFalling())
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
        }
    }
}
