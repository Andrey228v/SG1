using Assets.Scripts.GameSM;
using Zenject;

namespace Assets.Scripts.StateMachines.GameUISM.Starter
{
    public class GameUIStarter : IInitializable
    {
        private GameUIStateMachine _gameUIStateMachine;

        [Inject]
        public GameUIStarter(GameUIStateMachine gameUIStateMachine)
        {
            _gameUIStateMachine = gameUIStateMachine;
        }

        public void Initialize()
        {
            _gameUIStateMachine.ChooseState(GameUIStates.Game);
        }
    }
}
