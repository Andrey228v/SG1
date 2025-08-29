using Assets.Scripts.DetectorProperties;
using Assets.Scripts.StateMachineUnit;
using Assets.Scripts.Units.States;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [RequireComponent(typeof(PlayerView), typeof(AnimatorPersonController), typeof(GroundChecker))]
    [RequireComponent(typeof(SlopeChecker), typeof(PlayerStateMachine), typeof(SignalReader))]
    public class Unit : MonoBehaviour
    {
        public PlayerView PlayerView { get; private set; }
        public AnimatorPersonController AnimatorPersonController { get; private set; }
        public GroundChecker GroundChecker { get; private set; }
        public SlopeChecker SlopeChecker { get; private set; }
        public PlayerStateMachine PlayerStateMachine { get; private set; }
        public SignalReader SignalReader { get; private set; }
        public DragChecker DragChecker { get; private set; }

        public void Awake()
        {
            PlayerView = GetComponent<PlayerView>();
            AnimatorPersonController = GetComponent<AnimatorPersonController>();
            GroundChecker = GetComponent<GroundChecker>();
            SlopeChecker = GetComponent<SlopeChecker>();
            PlayerStateMachine = GetComponent<PlayerStateMachine>();
            SignalReader = GetComponent<SignalReader>();
        }

        private void OnEnable()
        {
            GroundChecker.OnGround += PlayerView.SetIsGround;
        }

        private void OnDisable()
        {
            GroundChecker.OnGround -= PlayerView.SetIsGround;
        }

        public void ProcessSignalDirection(Vector3 direction)
        {
            Vector3 normal = GroundChecker.GetGroundNormal();
            Vector3 project = Vector3.ProjectOnPlane(direction, normal).normalized;

            PlayerView.SetMoveDirection(project);
        }

        public void SetProcessSignalMove()
        {
            SignalReader.SetIsMove(true); 
        }

        public void SetProcessSignalStop()
        {
            SignalReader.SetIsMove(false);
        }

        public void ProcessSignalJump()
        {
            SignalReader.SetIsJump(true);
        }

        public void ProcessSignalJumpStope()
        {
            SignalReader.SetIsJump(false);
        }


    }
}
