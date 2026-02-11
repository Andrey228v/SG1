using Assets.Scripts.StateMachines.GameUISM.State;
using Zenject;

namespace Assets.Scripts.StateMachines.GameUISM
{

    public enum GameUIStates
    {
        Game,
        Menu,
    }


    public class GameUIStateMachine
    {
        public IEEState CurrentState { get; private set; }
        public IEEState PreviousState { get; private set; }

        private DiContainer _container;
        private IEEState _nextState;

        public GameUIStateMachine(DiContainer container)
        {
            _container = container;
            PreviousState = null;
        }

        private void ChangeState(IEEState newState)
        {
            PreviousState = CurrentState;
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void ChooseState(GameUIStates state)
        {
            switch (state)
            {
                case GameUIStates.Game:
                    _nextState = _container.Resolve<GameInterfaceState>();
                    ChangeState(_nextState);
                    break;

                case GameUIStates.Menu:
                    _nextState = _container.Resolve<GameMenuState>();
                    ChangeState(_nextState);
                    break;
            }
        }
    }
}
