using Assets.Scripts.Units;
using Assets.Scripts.Units.States;
using Assets.Scripts.Utilites;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachineUnit
{

    //1) ОНА УЖЕ НЕ ИСПОЛЬЗУЕТСЯ ИЗ-ЗА ВСТРЕННОЙ СТЕЙТ МАШИНЫ CHARACTER CONTROLLER...
    //2) ТЕСТЫ ПОКАЗАЛИ ЧТО ВСТРОЕННАЯ МАШИНА НЕ УДОБНАЯ И В НЕЙ РАЗБИРАТЬСЯ ДОЛГО, ПОЭТОМУ ОСТАВЛЯЕМ НАШУ...
    public class PlayerStateMachine : MonoBehaviour, IPause
    {
        private Unit _unit;
        private StayState _stayState;
        private RunState _runState;
        private JumpState _jumpState;
        private FallState _fallState;
        private bool _pause = false;

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
            if (_pause == false)
            {
                CurrentState.UpdateState();
            }
        }

        private void FixedUpdate()
        {
            if (_pause == false)
            {
                CurrentState.FixedUpdate();
            }
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
                    //Debug.Log(UnitStateType.Stay.ToString());
                    ChangeState(_stayState);
                    OnChangedState?.Invoke(UnitStateType.Stay.ToString());
                    break;

                case UnitStateType.Run:
                    //Debug.Log(UnitStateType.Run.ToString());
                    ChangeState(_runState);
                    OnChangedState?.Invoke(UnitStateType.Run.ToString());
                    break;

                case UnitStateType.Jump:
                    //Debug.Log(UnitStateType.Jump.ToString());
                    ChangeState(_jumpState);
                    OnChangedState?.Invoke(UnitStateType.Jump.ToString());
                    break;

                case UnitStateType.Fall:
                    //Debug.Log(UnitStateType.Fall.ToString());
                    ChangeState(_fallState);
                    OnChangedState?.Invoke(UnitStateType.Fall.ToString());
                    break;

                default:
                    //Console.WriteLine("None State");
                    break;
            }
        }

        public void Pause()
        {
            _pause = true;
        }

        public void Continue()
        {
            _pause = false;
        }
    }
}
