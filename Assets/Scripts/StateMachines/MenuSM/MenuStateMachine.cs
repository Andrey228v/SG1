using Assets.Scripts.StateMachines.MenuSM.States;
using System;
using Zenject;

namespace Assets.Scripts.StateMachines.MenuSM
{
    public enum MenuStates
    {
        Menu,
        NewGame,
        Continue,
        Settings,
        Exit,
        Previous,
    }

    public class MenuStateMachine
    {
        private IMenuState _nextState;
        
        private readonly DiContainer _container;

        public IMenuState CurrentState { get; private set; }
        public IMenuState PreviousState { get; private set; }

        public MenuStateMachine(DiContainer container)
        {
            _container = container;
            PreviousState = null;
        }

        private void ChangeState(IMenuState newState)
        {
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void ChooseState(MenuStates state)
        {
            switch (state)
            {
                case MenuStates.Menu:
                    _nextState = _container.Resolve<MenuWindowState>();
                    ChangeState(_nextState);
                    break;

                case MenuStates.NewGame:
                    _nextState = _container.Resolve<NewGameState>();
                    ChangeState(_nextState);
                    break;

                case MenuStates.Continue:
                    _nextState = _container.Resolve<ContinueGameState>();
                    ChangeState(_nextState);
                    break;

                case MenuStates.Settings:
                    _nextState = _container.Resolve<SettingState>();
                    ChangeState(_nextState);
                    break;

                case MenuStates.Exit:
                    _nextState = _container.Resolve<ExitState>();
                    ChangeState(_nextState);
                    break;

                case MenuStates.Previous:
                    ChangeState(PreviousState);
                    break;

                default:
                    throw new ArgumentException("State is not to be");
            }
        }
    }
}
