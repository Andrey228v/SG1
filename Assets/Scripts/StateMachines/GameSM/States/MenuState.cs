using Assets.Scripts.GameInstallers.Signals;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.GameSM.States
{
    public class MenuState : IGameState
    {
        private readonly SignalBus _signalBus;
        private readonly GameStateMachine _stateMachine;

        public MenuState(SignalBus signalBus, GameStateMachine stateMachine)
        {
            _signalBus = signalBus;
            _stateMachine = stateMachine;
        }


        public async Task Enter()
        {
            SceneManager.LoadScene(CONSTANTS.MENU);
            Debug.Log("MenuState: Начало инициализации...");
        }

        public async Task Exit()
        {
            
        }

        public async Task UpdateState()
        {
            
        }

        private void SubscribeToSignals()
        {

        }

        private async Task TransitionToGame()
        {
            Debug.Log("MenuState: Переход на уровень...");


        }

    }
}
