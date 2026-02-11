using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Units;
using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{

    //1) При отсутсвии сохранения загружать уровень со старта. + 
    //2) При удалении сохранений сбрасывать прогресс до начала уровня.
    //3) Протестировать ES3
    //4) При рестарте необходимо возвращать все объекты к начальному состоянию: Чекпоинты, монетки и т.д. Надо придумать как ...
    public class SaveLoadService : ISaveLoadService, IInitializable, IDisposable
    {
        private const string SAVE_KEY = "game_save";
        private const string SAVE_CHECK_POINTS = "checkPoinst_save";
        private const string SAVE_COINS_DATA = "coinsData_save";
        private const string SAVE_KEY_PROGRESS = "game_save_progress";
        private const string CURRENT_SAVE_VERSION = "1.0";

        private SignalBus _signalBus;
        private bool _isFirstLoad = true;
        public GameSaveData CurrentSave { get; private set; }

        public event Action OnGameSaved;
        public event Action OnGameLoaded;

        [Inject]
        public SaveLoadService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnGameInitialized>(LoadGame);
            _signalBus.Subscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            _signalBus.Subscribe<CoinCollectedSignal>(OnCoinCollected);
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Subscribe<OnMenuLoadGameClickedSignal>(LoadGame);
            _signalBus.Subscribe<OnCheckPointActivated>(SaveGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnGameInitialized>(LoadGame);
            _signalBus.Unsubscribe<CoinCollectedSignal>(OnCoinCollected);
            _signalBus.Unsubscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Unsubscribe<OnMenuLoadGameClickedSignal>(LoadGame);
            _signalBus.Unsubscribe<OnCheckPointActivated>(SaveGame);

            SaveGame();
        }

        public void SaveProgress(PlayerProgress progress)
        {
            string json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(SAVE_KEY_PROGRESS, json);
            PlayerPrefs.Save();
        }

        public void SaveGame()
        {
            if (CurrentSave == null)
            {
                Debug.LogWarning("No save data to save!");
                CreateNewSave(); // &&&&...
                return;
            }

            CurrentSave.SaveVersion = CURRENT_SAVE_VERSION;
            CurrentSave.SaveTime = DateTime.Now;
            _signalBus.Fire(new GameSavedSignal());

            string json = JsonUtility.ToJson(CurrentSave, true);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
        }

        public bool HasSave()
        {
            return PlayerPrefs.HasKey(SAVE_KEY);
        }

        public bool IsFirstLoad()
        {
            return _isFirstLoad;
        }

        //Загрузка прогресса... 
        //Надо ли это пока не знаю...
        //public PlayerProgress LoadProgress()
        //{
        //    if (HasSave() == false)
        //    {
        //        return new PlayerProgress();
        //    }

        //    _isFirstLoad = false;
        //    string json = PlayerPrefs.GetString(SAVE_KEY_PROGRESS);
        //    return JsonUtility.FromJson<PlayerProgress>(json);
        //}

        public void LoadGame()
        {
            if (HasSave() == false) 
            {
                //CurrentSave = new GameSaveData(); // тут ошибка. Затираются данные.
                _signalBus.Fire(new GameLoadedSignal());
                return;
            }
            _isFirstLoad = false;

            string json = PlayerPrefs.GetString(SAVE_KEY);
            CurrentSave = JsonUtility.FromJson<GameSaveData>(json);

            _signalBus.Fire(new GameLoadedSignal());
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SAVE_KEY);
            CurrentSave = new GameSaveData();
            _isFirstLoad = true;
        }

        public void CreateNewSave()
        {
            //CurrentSave.Restart();

            //LevelData = new LevelSaveData(),
            //ProgressData = new GameProgressData()
        }

        private void OnCoinCollected(CoinCollectedSignal signal)
        {
            //if (_currentSave == null) return;

            //_currentSave.PlayerData.CoinsCollected++;
            //_currentSave.ProgressData.TotalCoins++;

            //if (!_currentSave.PlayerData.CollectedCoins.Contains(signal.CoinId))
            //{
            //    _currentSave.PlayerData.CollectedCoins.Add(signal.CoinId);
            //}

            // Авто-сохранение при сборе монеты
            //SaveGame();
        }

        private void OnCheckpointActivated(CheckpointActivatedSignal signal)
        {
            if (CurrentSave == null)
            {
                return;
            }

            SaveGame();
        }

        private void OnPlayerDied(PlayerDiedSignal signal)
        {
            //if (_currentSave == null) return;

            //_currentSave.ProgressData.TotalDeaths++;
        }

        private void OnGameSaveRequested(GameSavedSignal signal)
        {
            SaveGame();

            //string json = JsonUtility.ToJson(_currentSave, true);

            //Debug.Log(json);

            //PlayerPrefs.SetString(SAVE_KEY, json);
            //PlayerPrefs.Save();
        }

        
    }
}
