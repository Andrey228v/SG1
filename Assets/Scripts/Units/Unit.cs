using Assets.Scripts.DetectorProperties;
using Assets.Scripts.DetectorProperties.GroundCheckerStrategy;
using Assets.Scripts.PlayerSettings;
using Assets.Scripts.StateMachineUnit;
using Assets.Scripts.Units.States;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [RequireComponent(typeof(PlayerView), typeof(AnimatorPersonController))]
    [RequireComponent(typeof(PlayerStateMachine), typeof(SignalReader))]
    [RequireComponent(typeof(GravityChecker))]
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] public UnitSetting Settings { get; private set; }

        public PlayerView PlayerView { get; private set; }
        public AnimatorPersonController AnimatorPersonController { get; private set; }
        //public AGroundCheckerStrategy AGroundChecker { get; private set; }
        public GravityChecker GravityChecker { get; private set; }
        //public GroundChecker GroundChecker { get; private set; }
        //public SlopeChecker SlopeChecker { get; private set; }
        public PlayerStateMachine PlayerStateMachine { get; private set; }
        public SignalReader SignalReader { get; private set; }
        public DragChecker DragChecker { get; private set; }

        public void Awake()
        {
            PlayerView = GetComponent<PlayerView>();
            AnimatorPersonController = GetComponent<AnimatorPersonController>();
            GravityChecker = GetComponent<GravityChecker>();
            //GroundChecker = GetComponent<GroundChecker>();
            //SlopeChecker = GetComponent<SlopeChecker>();
            PlayerStateMachine = GetComponent<PlayerStateMachine>();
            SignalReader = GetComponent<SignalReader>();
        }

        private void OnEnable()
        {
            Settings.AGroundChecker.OnGround += PlayerView.SetIsGround;
            Settings.AGroundChecker.OnGround += GravityChecker.SetIsGround;
        }

        private void OnDisable()
        {
            Settings.AGroundChecker.OnGround -= PlayerView.SetIsGround;
            Settings.AGroundChecker.OnGround -= GravityChecker.SetIsGround;
        }

        private void Update()
        {
            Settings.AGroundChecker.CheckGround(transform);
        }

        private void OnDrawGizmos()
        {
            Settings.AGroundChecker.OnDrawGizmos(transform);
        }

        public void ProcessSignalDirection(Vector3 direction)
        {
            Vector3 normal = Settings.AGroundChecker.GetGroundNormal();
            direction = Vector3.ProjectOnPlane(direction, normal).normalized;

            PlayerView.SetMoveDirection(direction);
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
