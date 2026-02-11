using Zenject;

namespace Assets.Scripts.StateMachines.MenuSM.Starter
{
    public class MenuStarter : IInitializable
    {
        private MenuStateMachine _menuStateMachine;

        public MenuStarter(MenuStateMachine menuStateMachine)
        {
            _menuStateMachine = menuStateMachine;
        }

        public void Initialize()
        {
            _menuStateMachine.ChooseState(MenuStates.Menu);
        }
    }
}
