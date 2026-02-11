using Assets.Scripts.Services;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.GameSM.States
{
    public class LoadGameState : IGameState
    {
        private readonly SaveLoadService _saveLoadService;
        //private readonly IAssetLoader _assetLoader;
        //private readonly SceneLoader _sceneLoader;
        private readonly SignalBus _signalBus;
        private readonly GameStateMachine _stateMachine;
        private readonly PlayerProgress _playerProgress;
        private readonly GameSettings _gameSettings;

        [Inject]
        public LoadGameState(SaveLoadService saveLoadService, SignalBus signalBus, GameStateMachine stateMachineGame, PlayerProgress playerProgress, GameSettings gameSettings)
        {
            _saveLoadService = saveLoadService;
            _signalBus = signalBus;
            //_sceneLoader = sceneLoader;
            _stateMachine = stateMachineGame;
            _playerProgress = playerProgress;
            _gameSettings = gameSettings;
        }

        public async Task Enter()
        {
            //Debug.Log("LoadGameState: Начало инициализации...");

            //try
            //{
            //    // 1. Инициализация систем в строгом порядке
            //    await InitializeSystems();

            //    // 2. Загрузка сохранений
            //    await LoadPlayerProgress();

            //    // 3. Настройка игры
            //    ApplyGameSettings();

            //    // 4. Подписка на события
            //    SubscribeToSignals();

            //    // 5. Переход в меню
            //    await TransitionToMenu();

            //    Debug.Log("LoadGameState: Инициализация завершена успешно");
            //}
            //catch (Exception ex)
            //{
            //    Debug.LogError($"LoadGameState: Ошибка инициализации: {ex.Message}");
            //    HandleBootError(ex);
            //}
        }

        public async Task UpdateState()
        {
            // Переходим в состояние меню
            await _stateMachine.Enter<MenuState>();
        }

        public Task Exit()
        {
            //Debug.Log("LoadGameState: Выход из состояния");

            //// Отписываемся от сигналов
            //_signalBus.Unsubscribe<ApplicationFocusSignal>(OnApplicationFocus);
            //_signalBus.Unsubscribe<ApplicationPauseSignal>(OnApplicationPause);
            //_signalBus.Unsubscribe<ApplicationQuitSignal>(OnApplicationQuit);

            //// Освобождаем ресурсы
            //Resources.UnloadUnusedAssets();

            return Task.CompletedTask;
        }

        //private async Task InitializeSystems()
        //{
        //    // 1. Проверка системных требований
        //    if (!CheckSystemRequirements())
        //    {
        //        throw new Exception("Системные требования не удовлетворены");
        //    }

        //    // 2. Инициализация загрузчика ресурсов
        //    //await _assetLoader.Initialize();

        //    // 3. Предзагрузка критичных ресурсов
        //    await PreloadCriticalAssets();

        //    // 4. Настройка качества графики
        //    ConfigureQualitySettings();

        //    // 5. Инициализация аудио системы
        //    InitializeAudio();

        //    // 6. Настройка ввода
        //    ConfigureInput();

        //    Debug.Log("BootState: Системы инициализированы");
        //}

        //private async Task LoadPlayerProgress()
        //{
        //    Debug.Log("BootState: Загрузка прогресса игрока...");

        //    try
        //    {
        //        if (_saveLoadService.HasSave())
        //        {
        //            var loadedProgress = _saveLoadService.LoadProgress();

        //            // Валидация загруженных данных
        //            if (IsValidProgress(loadedProgress))
        //            {
        //                // Копируем валидные данные
        //                _playerProgress.CurrentLevel = loadedProgress.CurrentLevel;
        //                _playerProgress.CompletedLevels = loadedProgress.CompletedLevels;
        //                _playerProgress.TotalScore = loadedProgress.TotalScore;
        //                _playerProgress.UnlockedRewards = loadedProgress.UnlockedRewards;

        //                Debug.Log($"BootState: Прогресс загружен. Уровень: {_playerProgress.CurrentLevel}");
        //            }
        //            else
        //            {
        //                Debug.LogWarning("BootState: Некорректные данные сохранения. Используются значения по умолчанию");
        //                CreateDefaultProgress();
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("BootState: Сохранения не найдены. Создание нового профиля");
        //            CreateDefaultProgress();

        //            // Сохраняем дефолтный прогресс
        //            _saveLoadService.SaveProgress(_playerProgress);
        //        }

        //        await Task.Yield(); // Для асинхронности
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.LogError($"BootState: Ошибка загрузки прогресса: {ex.Message}");
        //        CreateDefaultProgress();
        //    }
        //}

        //private void ApplyGameSettings()
        //{
        //    Debug.Log("BootState: Применение настроек игры...");

        //    // Применяем настройки из сохранения или дефолтные
        //    _playerProgress.Settings ??= new StartGameSettings();

        //    // Применяем настройки аудио
        //    AudioListener.volume = _playerProgress.Settings.MasterVolume;

        //    // Устанавливаем язык
        //    ApplyLanguageSettings();

        //    // Применяем графические настройки
        //    ApplyGraphicsSettings();

        //    Debug.Log("BootState: Настройки применены");
        //}

        //private async Task PreloadCriticalAssets()
        //{
        //    //Предзагрузка критически важных ассетов
        //    //var preloadTasks = new[]
        //    //{
        //    //    _assetLoader.PreloadAsset<GameObject>("UI/Prefabs/LoadingScreen"),
        //    //    _assetLoader.PreloadAsset<GameObject>("UI/Prefabs/ErrorPopup"),
        //    //    _assetLoader.PreloadAsset<AudioClip>("Audio/UI/Click"),
        //    //    _assetLoader.PreloadAsset<Sprite>("UI/Sprites/DefaultIcon")
        //    //};

        //    //await Task.WhenAll(preloadTasks);
        //}

        //Нужно ли... Пока не понятно...
        //private bool CheckSystemRequirements()
        //{

        //    //// Проверка поддержки графики
        //    //if (!SystemInfo.supportsComputeShaders)
        //    //{
        //    //    Debug.LogWarning("BootState: Compute Shaders не поддерживаются");
        //    //}

        //    //// Проверка памяти
        //    //if (SystemInfo.systemMemorySize < 2048)
        //    //{
        //    //    Debug.LogWarning("BootState: Мало оперативной памяти (< 2GB)");
        //    //    return false;
        //    //}

        //    return true;
        //}

        //Нужно ли... Пока не понятно...
        //        private void ConfigureQualitySettings()
        //        {
        ////            // Автонастройка качества графики
        ////            int memorySize = SystemInfo.systemMemorySize;
        ////            string gpuName = SystemInfo.graphicsDeviceName;

        ////            if (memorySize > 8192 && gpuName.Contains("RTX"))
        ////            {
        ////                QualitySettings.SetQualityLevel(2); // Высокое
        ////            }
        ////            else if (memorySize > 4096)
        ////            {
        ////                QualitySettings.SetQualityLevel(1); // Среднее
        ////            }
        ////            else
        ////            {
        ////                QualitySettings.SetQualityLevel(0); // Низкое
        ////            }

        ////            // Оптимизации для мобильных платформ


        //            //Debug.Log($"BootState: Качество графики установлено на {QualitySettings.names[QualitySettings.GetQualityLevel()]}");
        //        }

        //Нужно ли... Пока не понятно...
        //private void InitializeAudio()
        //{
        //    // Инициализация аудио миксера если используется
        //    #if UNITY_AUDIO_MODULE
        //    var audioMixer = Resources.Load<UnityEngine.Audio.AudioMixer>("Audio/MainMixer");
        //    if (audioMixer != null)
        //    {
        //        Debug.Log("BootState: Аудио микшер загружен");
        //    }
        //    #endif
        //}

        //Нужно ли... Пока не понятно...
        //private void ConfigureInput()
        //{
        //    // Настройка системы ввода
        //    Input.multiTouchEnabled = false; // Отключаем мультитач если не нужно

        //    #if ENABLE_INPUT_SYSTEM
        //    //Debug.Log("BootState: Используется новая система ввода");
        //    #else
        //    Debug.Log("BootState: Используется старая система ввода");
        //    #endif
        //}

        //private bool IsValidProgress(PlayerProgress progress)
        //{
        //    if (progress == null) return false;

        //    // Проверяем базовую валидность данных
        //    bool isValid = true;

        //    // CurrentLevel должен быть положительным
        //    isValid &= progress.CurrentLevel > 0;
        //    isValid &= progress.CurrentLevel <= _gameSettings.MaxLevels;

        //    // TotalScore не может быть отрицательным
        //    isValid &= progress.TotalScore >= 0;

        //    // CompletedLevels не должны содержать дубликаты
        //    var distinctLevels = new HashSet<int>(progress.CompletedLevels);
        //    isValid &= distinctLevels.Count == progress.CompletedLevels.Count;

        //    // Все завершенные уровни должны быть в допустимом диапазоне
        //    foreach (var level in progress.CompletedLevels)
        //    {
        //        isValid &= level > 0 && level <= _gameSettings.MaxLevels;
        //    }

        //    return isValid;
        //}

        //private void CreateDefaultProgress()
        //{
        //    _playerProgress.CurrentLevel = 1;
        //    _playerProgress.CompletedLevels = new List<int>();
        //    _playerProgress.TotalScore = 0;
        //    _playerProgress.UnlockedRewards = new List<string>();
        //    _playerProgress.Settings = new StartGameSettings
        //    {
        //        MasterVolume = 0.8f,
        //        MusicVolume = 0.6f,
        //        SFXVolume = 0.7f,
        //        Language = Application.systemLanguage.ToString(),
        //        QualityLevel = QualitySettings.GetQualityLevel()
        //    };
        //}

        //private void ApplyLanguageSettings()
        //{
        //    // Установка языка игры
        //    string language = _playerProgress.Settings.Language;

        //    try
        //    {
        //        // Можно использовать I2 Localization или аналоги
        //        // LocalizationManager.SetLanguage(language);
        //        Debug.Log($"BootState: Язык установлен: {language}");
        //    }
        //    catch
        //    {
        //        Debug.LogWarning("BootState: Не удалось установить язык");
        //    }
        //}

        //Нужно ли... Пока не понятно...
        //private void ApplyGraphicsSettings()
        //{
        //    //// Применение настроек графики
        //    //QualitySettings.SetQualityLevel(_playerProgress.Settings.QualityLevel);

        //    //// Настройка разрешения
        //    //if (_playerProgress.Settings.TargetResolution.HasValue)
        //    //{
        //    //    var resolution = _playerProgress.Settings.TargetResolution.Value;
        //    //    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //    //}

        //    //// Настройка полного экрана
        //    //Screen.fullScreen = _playerProgress.Settings.IsFullscreen;
        //}

        //private void SubscribeToSignals()
        //{
        //    // Подписка на системные сигналы
        //    _signalBus.Subscribe<ApplicationFocusSignal>(OnApplicationFocus);
        //    _signalBus.Subscribe<ApplicationPauseSignal>(OnApplicationPause);
        //    _signalBus.Subscribe<ApplicationQuitSignal>(OnApplicationQuit);

        //    //Debug.Log("LoadGameState: Подписка на сигналы выполнена");
        //}

        //private async Task TransitionToMenu()
        //{
        //    Debug.Log("LoadGameState: Переход в главное меню...");

        //    // Небольшая задержка для стабильности (опционально)
        //    await Task.Delay(500);

        //    //Отправляем сигнал о завершении загрузки
        //    _signalBus.Fire(new BootCompleteSignal
        //    {
        //        LoadTime = Time.realtimeSinceStartup,
        //        HasSave = _saveLoadService.HasSave()
        //    });


        //}

        //private void HandleBootError(Exception ex)
        //{
        //    Debug.LogError($"LoadGameState: Критическая ошибка: {ex}");

        //    // Можно показать экран ошибки
        //    // _uiFactory.CreateErrorScreen("Ошибка загрузки", ex.Message);

        //    // Или попробовать восстановиться
        //    //CreateDefaultProgress();
        //    //_stateMachine.Enter<MenuState>();
        //}

        //private void OnApplicationFocus(ApplicationFocusSignal signal)
        //{
        //    Debug.Log($"LoadGameState: Фокус приложения: {signal.HasFocus}");

        //    //if (signal.HasFocus)
        //    //{
        //    //    // Возобновляем игру
        //    //    Time.timeScale = 1f;
        //    //}
        //}

        //private void OnApplicationPause(ApplicationPauseSignal signal)
        //{
        //    Debug.Log($"LoadGameState: Пауза приложения: {signal.IsPaused}");

        //    //if (signal.IsPaused)
        //    //{
        //    //    // Автосохранение при паузе
        //    //    _saveService.SaveProgress(_playerProgress);
        //    //}
        //}

        //private void OnApplicationQuit(ApplicationQuitSignal signal)
        //{
        //    Debug.Log("LoadGameState: Выход из приложения...");

        //    //// Сохранение перед выходом
        //    //_saveService.SaveProgress(_playerProgress);

        //    //// Очистка ресурсов
        //    //_assetLoader.Cleanup();
        //}


    }
}
