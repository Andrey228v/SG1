using Assets.Scripts.PlayerSettings;
using Assets.Scripts.Units;
using Assets.Scripts.Units.States;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachineUnit
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private Unit _unit;
        private StayState _stayState;
        private RunState _runState;
        private JumpState _jumpState;
        private FallState _fallState;

        public event Action<string> OnChangedState;

        public IStateUnit CurrentState { get; private set; }

        private void Awake()
        {
            _unit = GetComponent<Unit>();

            _stayState = new StayState(this, _unit);
            _runState = new RunState(this, _unit);
            _jumpState = new JumpState(this, _unit);
            _fallState = new FallState(this, _unit);
        }

        private void Start()
        {
            CurrentState = _stayState;
            SelectState(UnitStateType.Stay);
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }

        private void ChangeState(IStateUnit newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void SelectState(UnitStateType stateType)
        {
            switch (stateType)
            {
                case UnitStateType.Stay:
                    Debug.Log(UnitStateType.Stay.ToString());
                    ChangeState(_stayState);
                    OnChangedState?.Invoke(UnitStateType.Stay.ToString());
                    break;

                case UnitStateType.Run:
                    Debug.Log(UnitStateType.Run.ToString());
                    ChangeState(_runState);
                    OnChangedState?.Invoke(UnitStateType.Run.ToString());
                    break;

                case UnitStateType.Jump:
                    Debug.Log(UnitStateType.Jump.ToString());
                    ChangeState(_jumpState);
                    OnChangedState?.Invoke(UnitStateType.Jump.ToString());
                    break;

                case UnitStateType.Fall:
                    Debug.Log(UnitStateType.Fall.ToString());
                    ChangeState(_fallState);
                    OnChangedState?.Invoke(UnitStateType.Fall.ToString());
                    break;

                default:
                    Console.WriteLine("None State");
                    break;
            }
        }
    }
}
