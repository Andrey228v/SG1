using Assets.Scripts.UI.Menu;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.StateMachines.MenuSM.States
{
    public class MenuWindowState : IMenuState
    {
        private MainMenuPanel _menuWindowPanel;

        public MenuWindowState(MainMenuPanel mainMenuPanel)
        {
            _menuWindowPanel = mainMenuPanel;
        }

        public void Enter()
        {
            Debug.Log("Заход в меню");
            _menuWindowPanel.Show();
        }

        public void Exit()
        {
            _menuWindowPanel.Hide();
        }
    }
}
