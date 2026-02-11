using Assets.Scripts.UI._2_GamePanelWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachines.GameUISM.State
{
    public class GameMenuState : IEEState
    {
        private GameMenuPanel _gameMenuPanel;

        public GameMenuState(GameMenuPanel gameMenuPanel)
        {
            _gameMenuPanel = gameMenuPanel;
        }


        public void Enter()
        {
            //снять паузу, если она стоит.
            _gameMenuPanel.Show();

        }

        public void Exit() 
        {
            // Поставить паузу и выйти из состояния.
            _gameMenuPanel.Hide();
        }

    }
}
