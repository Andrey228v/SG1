using Assets.Scripts.PlayerSettings;
using Assets.Scripts.StateMachineUnit;
using System;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [RequireComponent(typeof(PlayerView), typeof(AnimatorPersonController))]
    [RequireComponent(typeof(PlayerStateMachine), typeof(SignalReader))]
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] public UnitSetting Settings { get; private set; }
        public PlayerView PlayerView { get; private set; }
        public AnimatorPersonController AnimatorPersonController { get; private set; }
        public PlayerStateMachine PlayerStateMachine { get; private set; }
        public SignalReader SignalReader { get; private set; }

        public event Action<bool> OnJumpButtonDown;
        public event Action<bool> OnJumpButtonUp;

        public void Awake()
        {
            PlayerView = GetComponent<PlayerView>();
            AnimatorPersonController = GetComponent<AnimatorPersonController>();
            PlayerStateMachine = GetComponent<PlayerStateMachine>();
            SignalReader = GetComponent<SignalReader>();
        }

        public void ProcessSignalDirection(Vector3 direction)
        {
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

        public void ProcessSignalJumpButtonDown(bool isDown)
        {
            SignalReader.SetIsJumpButtonDown(isDown);
            OnJumpButtonDown?.Invoke(isDown);
        }

        public void ProcessSignalJumpButtonUp(bool isUp)
        {
            SignalReader.SetIsJumpButtonUp(isUp);
            OnJumpButtonUp?.Invoke(isUp);
        }
    }
}
