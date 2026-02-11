using Assets.Scripts.UI.GameSettings;
using UnityEngine;

namespace Assets.Scripts.StateMachines.MenuSM.States
{
    public class SettingState : IMenuState
    {
        private SettingsPanel _settingsPanel;

        public SettingState(SettingsPanel settingsPanel) 
        {
            _settingsPanel = settingsPanel;
        }

        public void Enter()
        {
            _settingsPanel.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _settingsPanel.gameObject.SetActive(false);
        }
    }
}
