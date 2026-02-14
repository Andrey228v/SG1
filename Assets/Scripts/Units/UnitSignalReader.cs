using Assets.Scripts.PlayerSettings;
using Assets.Scripts.StateMachineUnit;
using Assets.Scripts.Utilites;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Units
{
    //[RequireComponent(typeof(PlayerView), typeof(AnimatorPersonController))]
    //[RequireComponent(typeof(PlayerStateMachine), typeof(SignalReader))]
    //public class Unit : MonoBehaviour, IPause
    public class UnitSignalReader : IPause
    {
        public UnitSetting Settings { get; private set; }
        public PlayerView PlayerView { get; private set; }
        public AnimatorPersonController AnimatorPersonController { get; private set; }
        public UnitStateMachine UnitStateMachine { get; private set; }
        public SignalReader SignalReader { get; private set; }

        public event Action<Vector3> OnChangePosition;
        public event Action<bool> OnJumpButtonDown;
        public event Action<bool> OnJumpButtonUp;

        public UnitSignalReader(PlayerView playerView, AnimatorPersonController animatorPersonController, UnitStateMachine unitStateMachine, SignalReader signalReader)
        {
            PlayerView = playerView;
            AnimatorPersonController = animatorPersonController;
            UnitStateMachine = unitStateMachine;
            SignalReader = signalReader;
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

        public void Pause()
        {
            PlayerView.Pause();
            UnitStateMachine.Pause();
        }

        public void Continue()
        {
            PlayerView.Continue();
            UnitStateMachine.Continue();
        }

        public class Factory : PlaceholderFactory<UnitSignalReader> { }
    }
}
