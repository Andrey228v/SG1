using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.States;
using Assets.Scripts.Services.Save;
using Assets.Scripts.StateMachines.GameUISM;
using Assets.Scripts.UI.Load;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI._2_GamePanelWindow
{
    public class GamePanelController : IInitializable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameUIStateMachine _gameUIMachine;
        private readonly SignalBus _signalBus;
        private readonly SaveLoadService _saveLoadService;

        private bool _isInitialized;

        [Inject]
        public GamePanelController(GameStateMachine gameStateMachine, GameUIStateMachine gameUIMachine, SignalBus signalBus, SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _gameUIMachine = gameUIMachine;
            _signalBus = signalBus;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            Debug.Log("[MenuController] Инициализация главного меню");

            SubscribeToSignals();

            _isInitialized = true;
        }
            
        public void Dispose()
        {
            _signalBus.Unsubscribe<OnMenuInGameClickedSignal>(OnMenuCallClicked);
            //2
            //3

            _signalBus.Unsubscribe<OnBackButtonGameClickedSignal>(OnBackButtonClicked);
            _signalBus.Unsubscribe<OnExitButtonGameClickedSignal>(OnExitButtonClick);
        }

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<OnMenuInGameClickedSignal>(OnMenuCallClicked);
            //2
            //3

            _signalBus.Subscribe<OnBackButtonGameClickedSignal>(OnBackButtonClicked);
            _signalBus.Subscribe<OnExitButtonGameClickedSignal>(OnExitButtonClick);
        }



        private void OnMenuCallClicked()
        {
            Debug.Log("Нажата кнопка вызова меню из игры");
            _gameUIMachine.ChooseState(GameUIStates.Menu);
        }

        private void OnLoadCallClicked()
        {
            //Загружаем последнюю точку.
        }

        private void OnSoundCallClicked()
        {
            //отключаем музыку или включаем
        }

        private void OnBackButtonClicked()
        {
            //снять паузу
            _gameUIMachine.ChooseState(GameUIStates.Game);
        }

        private async void OnExitButtonClick()
        {
            //Save
            await _gameStateMachine.Enter<MenuState>();
        }
    }
}
