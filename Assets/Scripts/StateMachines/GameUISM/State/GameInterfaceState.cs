using Assets.Scripts.UI._2_GamePanelWindow;


namespace Assets.Scripts.StateMachines.GameUISM.State
{
    public class GameInterfaceState : IEEState
    {
        private GameInterfacePanel _gameInterfacePanel;

        public GameInterfaceState(GameInterfacePanel gameInterfacePanel)
        {
            _gameInterfacePanel = gameInterfacePanel;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
