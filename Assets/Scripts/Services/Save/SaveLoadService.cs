using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class SaveLoadService : ISaveLoadService, IInitializable
    {
        private const string SAVE_KEY = "game_save";

        private SignalBus _signalBus;
        public GameSaveData CurrentSave { get; private set; }

        public bool HasSave => PlayerPrefs.HasKey(SAVE_KEY);
        //public GameSaveData CurrentSave => _currentSave;

        public event Action OnGameSaved;
        public event Action OnGameLoaded;

        [Inject]
        public SaveLoadService(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<CoinCollectedSignal>(OnCoinCollected);
            _signalBus.Subscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Initialize()
        {
            //_signalBus.Subscribe<CoinCollectedSignal>(OnCoinCollected);
            //_signalBus.Subscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            //_signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);

            //LoadGame();

            //if (_currentSave == null)
            //{
            //    CreateNewSave();
            //}
            //else
            //{
            //    LoadGame();
            //}

            //_signalBus.Subscribe<GameSavedSignal>(OnGameSaveRequested);
        }

        public void SaveGame()
        {
            if (CurrentSave == null)
            {
                Debug.LogWarning("No save data to save!");
                CreateNewSave(); // &&&&...
                return;
            }

            CurrentSave.SaveTime = DateTime.Now;
            _signalBus.Fire(new GameSavedSignal());

            string json = JsonUtility.ToJson(CurrentSave, true);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();

            //_signalBus.Fire(new GameSavedSignal());
        }

        public void LoadGame()
        {
            if (!HasSave) // Надо ли это ??....
            {
                CreateNewSave();
                return;
            }

            string json = PlayerPrefs.GetString(SAVE_KEY);
            CurrentSave = JsonUtility.FromJson<GameSaveData>(json);

            //string json = ES3.Load<String>(SAVE_KEY);
            //_currentSave = JsonUtility.FromJson<GameSaveData>(json); ///

            //OnGameLoaded?.Invoke();
            _signalBus.Fire(new GameLoadedSignal());
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SAVE_KEY);
            CurrentSave = null;
        }

        public void CreateNewSave()
        {

            CurrentSave = new GameSaveData
            {
                SaveTime = DateTime.Now,
                PlayerData = new PlayerSaveData(),
            };

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
            //if (_currentSave == null) return;

            //_currentSave.PlayerData.CurrentCheckpointId = signal.CheckpointId;
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
