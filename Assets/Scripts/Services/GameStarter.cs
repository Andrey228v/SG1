using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.States;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services
{
    public class GameStarter : IInitializable
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public GameStarter(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            Start();
            
        }

        private async void Start()
        {
            Debug.Log("1)GameStarter: Запуск начального состояния...");
            await _gameStateMachine.Enter<LoadGameState>();
        }
    }
}
