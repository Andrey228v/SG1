using Assets.Scripts.Units.States;
using Assets.Scripts.Utilites;
using System;
using Zenject;

namespace Assets.Scripts.StateMachineUnit
{

    //1) ОНА УЖЕ НЕ ИСПОЛЬЗУЕТСЯ ИЗ-ЗА ВСТРЕННОЙ СТЕЙТ МАШИНЫ CHARACTER CONTROLLER...
    //2) ТЕСТЫ ПОКАЗАЛИ ЧТО ВСТРОЕННАЯ МАШИНА НЕ УДОБНАЯ И В НЕЙ РАЗБИРАТЬСЯ ДОЛГО, ПОЭТОМУ ОСТАВЛЯЕМ НАШУ...
    public class UnitStateMachine : ITickable, IFixedTickable, IInitializable, IPause
    {
        private IStateUnit _nextState;
        private bool _pause = false;
        private readonly DiContainer _container;

        public event Action<string> OnChangedState;

        public IStateUnit CurrentState { get; private set; }
        public IStateUnit PreviousState { get; private set; }

        public UnitStateMachine(DiContainer container)
        {
            _container = container;
            PreviousState = null;
        }

        public void Initialize()
        {
            CurrentState = _container.Resolve<StayState>();
            SelectState(UnitStateType.Stay);
        }

        public void Tick()
        {
            if (_pause == false)
            {
                CurrentState.UpdateState();
            }
        }

        public void FixedTick()
        {
            if (_pause == false)
            {
                CurrentState.FixedUpdate();
            }
        }

        private void ChangeState(IStateUnit newState)
        {
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void SelectState(UnitStateType stateType)
        {
            switch (stateType)
            {
                case UnitStateType.Stay:
                    _nextState = _container.Resolve<StayState>();
                    ChangeState(_nextState);
                    OnChangedState?.Invoke(UnitStateType.Stay.ToString());
                    break;

                case UnitStateType.Run:
                    _nextState = _container.Resolve<RunState>();
                    ChangeState(_nextState);
                    OnChangedState?.Invoke(UnitStateType.Run.ToString());
                    break;

                case UnitStateType.Jump:
                    _nextState = _container.Resolve<JumpState>();
                    ChangeState(_nextState);
                    OnChangedState?.Invoke(UnitStateType.Jump.ToString());
                    break;

                case UnitStateType.Fall:
                    _nextState = _container.Resolve<FallState>();
                    ChangeState(_nextState);
                    OnChangedState?.Invoke(UnitStateType.Fall.ToString());
                    break;

                default:
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
