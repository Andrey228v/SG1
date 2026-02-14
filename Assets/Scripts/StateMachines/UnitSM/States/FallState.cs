using Assets.Scripts.StateMachineUnit;

namespace Assets.Scripts.Units.States
{
    public class FallState : IStateUnit
    {
        private UnitSignalReader _unitSignalReader;
        private UnitStateMachine _playerStateMachine;

        public FallState(UnitStateMachine playerStateMachine, UnitSignalReader unitSignalReader)
        {
            _unitSignalReader = unitSignalReader;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            _unitSignalReader.AnimatorPersonController.SetFall(true);
        }

        public void Exit()
        {
            _unitSignalReader.AnimatorPersonController.SetFall(false);
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
            if (_unitSignalReader.PlayerView.CharacterView.IsOnGround())
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unitSignalReader.SignalReader.GetIsJumpButtonDown() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Jump);
            }
        }
    }
}
