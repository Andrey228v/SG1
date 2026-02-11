using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.States;
using Assets.Scripts.Services.Save;
using Assets.Scripts.StateMachines.MenuSM;
using Assets.Scripts.UI.Load;
using Assets.Scripts.Units;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Menu
{
    public class MenuController : IInitializable, IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MenuStateMachine _menuStateMachine;
        private readonly SignalBus _signalBus;
        private readonly SaveLoadService _saveLoadService;
        private readonly LoadingScreen _loadingScreen;

        private bool _isInitialized;

        [Inject]
        public MenuController(GameStateMachine gameStateMachine,MenuStateMachine menuStateMachine, SignalBus signalBus, SaveLoadService saveLoadService, LoadingScreen loadingScreen)
        {
            _gameStateMachine = gameStateMachine;
            _menuStateMachine = menuStateMachine;
            _signalBus = signalBus;
            _loadingScreen = loadingScreen;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            SubscribeToSignals();
            _isInitialized = true;
        }

        private void SubscribeToSignals()
        {
            _signalBus.Subscribe<OnStartButtonClickedSignal>(OnStartButtonClicked);
            _signalBus.Subscribe<OnContinueButtonClickedSignal>(OnContinueButtonClicked);
            _signalBus.Subscribe<OnSettingsButtonClickedSignal>(OnSettingButtonClicked);
            _signalBus.Subscribe<OnExitButtonClickedSignal>(OnExitButtonClicked);
            _signalBus.Subscribe<OnBackButtonClickedSignal>(OnBackButtonClicked);
            _signalBus.Subscribe<OnDeletProgressClickedSignal>(OnDeletProgressButtonClicked);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnStartButtonClickedSignal>(OnStartButtonClicked);
            _signalBus.Unsubscribe<OnContinueButtonClickedSignal>(OnContinueButtonClicked);
            _signalBus.Unsubscribe<OnSettingsButtonClickedSignal>(OnSettingButtonClicked);
            _signalBus.Unsubscribe<OnExitButtonClickedSignal>(OnExitButtonClicked);
            _signalBus.Unsubscribe<OnBackButtonClickedSignal>(OnBackButtonClicked);
            _signalBus.Unsubscribe<OnDeletProgressClickedSignal>(OnDeletProgressButtonClicked);
        }

        private async void OnStartButtonClicked()
        {
            if (_isInitialized == false) return;

            Debug.Log("[MenuController] Нажата кнопка 'Начать'");

            _loadingScreen.Show("Загрузка новой игры...");

            // Создаем новую игру (удаляем старое сохранение)
            await StartNewGameAsync();
        }

        private async void OnContinueButtonClicked()
        {
            if (_isInitialized == false) return;

            Debug.Log("[MenuController] Нажата кнопка 'Продолжить'");

            if (_saveLoadService.HasSave() == false)
            {
                Debug.LogWarning("[MenuController] Нет сохранения для продолжения");
                return;
            }

            _loadingScreen.Show("Загрузка сохранения...");

            // Загружаем существующую игру
            await ContinueGameAsync();
        }

        private void OnSettingButtonClicked()
        {
            _menuStateMachine.ChooseState(MenuStates.Settings);
        }

        private void OnExitButtonClicked()
        {

        }

        private void OnBackButtonClicked()
        {
            _menuStateMachine.ChooseState(MenuStates.Previous);
        }

        private void OnDeletProgressButtonClicked()
        {
            _saveLoadService.DeleteSave();
        }

        private async Task StartNewGameAsync()
        {
            await _gameStateMachine.Enter<GameState>();
        }

        private async Task ContinueGameAsync()
        {
            try
            {
                // 1. Загружаем прогресс
                //var progress = _saveLoadService.LoadProgress();

                //if (progress == null)
                //{
                //    //_loadingScreen.ShowError("Сохранение повреждено");
                //    return;
                //}

                // 2. Применяем настройки из сохранения
                //_settingsManager.ApplySettings(progress.Settings);

                // 3. Небольшая задержка
                await Task.Delay(300);

                // 4. Переходим в геймплей
                await _gameStateMachine.Enter<GameState>();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[MenuController] Ошибка загрузки сохранения: {ex.Message}");
                //_loadingScreen.ShowError("Ошибка загрузки сохранения");
            }
        }

    }
}
