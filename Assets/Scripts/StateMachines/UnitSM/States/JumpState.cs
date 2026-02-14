using Assets.Scripts.StateMachineUnit;

namespace Assets.Scripts.Units.States
{
    public class JumpState : IStateUnit
    {
        private UnitSignalReader _unitSignalReader;
        private UnitStateMachine _playerStateMachine;

        public JumpState(UnitStateMachine playerStateMachine, UnitSignalReader unitSignalReader)
        {
            _unitSignalReader = unitSignalReader;
            _playerStateMachine = playerStateMachine;
        }

        public void Enter()
        {
            Jump();
        }

        public void Exit()
        {
            _unitSignalReader.PlayerView.StopJump();
            _unitSignalReader.AnimatorPersonController.SetJump(false);
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
            if (_unitSignalReader.SignalReader.IsMove == true && _unitSignalReader.PlayerView.CharacterView.IsOnGround() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Run);
            }
            else if (_unitSignalReader.SignalReader.IsMove == false && _unitSignalReader.PlayerView.CharacterView.IsOnGround() == true)
            {
                _playerStateMachine.SelectState(UnitStateType.Stay);
            }
            else if (_unitSignalReader.PlayerView.CharacterView.IsOnGround() == false && _unitSignalReader.PlayerView.GetIsFall())
            {
                _playerStateMachine.SelectState(UnitStateType.Fall);
            }
            else if (_unitSignalReader.SignalReader.GetIsJumpButtonDown() == true)
            {
                Jump();
            }
            else if (_unitSignalReader.SignalReader.GetIsJumpButtonUp())
            {
                _unitSignalReader.PlayerView.StopJump();
            }
        }

        private void Jump()
        {
            _unitSignalReader.PlayerView.Jump();
            _unitSignalReader.AnimatorPersonController.SetJump(true);
        }
    }
}
