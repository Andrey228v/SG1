using Assets.Scripts.Units;
using System;
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
        private const string SAVE_KEY_PROGRESS = "game_save_progress";

        private SignalBus _signalBus;
        public GameSaveData CurrentSave { get; private set; }

        public bool HasSave => PlayerPrefs.HasKey(SAVE_KEY);

        public event Action OnGameSaved;
        public event Action OnGameLoaded;

        [Inject]
        public SaveLoadService(SignalBus signalBus)
        {
            _signalBus = signalBus;
            CurrentSave = new GameSaveData();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            _signalBus.Subscribe<CoinCollectedSignal>(OnCoinCollected);
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);

            //LoadGame(); // Загружаться игра должна после того как прошла инициализация ...
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CoinCollectedSignal>(OnCoinCollected);
            _signalBus.Unsubscribe<CheckpointActivatedSignal>(OnCheckpointActivated);
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        //Сохранение прогресса ....
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

            CurrentSave.SaveTime = DateTime.Now;
            _signalBus.Fire(new GameSavedSignal());

            string json = JsonUtility.ToJson(CurrentSave, true);

            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
        }

        //Загрузка прогресса... 
        //Надо ли это пока не знаю...
        public PlayerProgress LoadProgress()
        {
            if (HasSave == false) return new PlayerProgress();

            string json = PlayerPrefs.GetString(SAVE_KEY_PROGRESS);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }

        public void LoadGame()
        {
            if (!HasSave) // Надо ли это ??....
            {
                _signalBus.Fire(new GameLoadedSignal());
                CreateNewSave();
                return;
            }

            string json = PlayerPrefs.GetString(SAVE_KEY);
            CurrentSave = JsonUtility.FromJson<GameSaveData>(json);

            //string json = ES3.Load<String>(SAVE_KEY);
            //_currentSave = JsonUtility.FromJson<GameSaveData>(json); ///

            //OnGameLoaded?.Invoke();

            //Тут отправляем сигнал всем кто должен загружаться ... 
            //Отправили игроку. А как загружать объекты...
            _signalBus.Fire(new GameLoadedSignal());
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SAVE_KEY);
            CurrentSave = null;
            _signalBus.Fire(new GameLoadedSignal());
        }

        public void CreateNewSave()
        {
            CurrentSave.Restart();

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

            if (CurrentSave == null) return;

            //_currentSave.PlayerData.CurrentCheckpointId = signal.CheckpointId;

            //CurrentSave.PlayerData;

            CurrentSave.CheckpointData.CheckpointId = signal.CheckpointId;
            CurrentSave.CheckpointData.Position = new Vector3Serializable(signal.Position);
            CurrentSave.CheckpointData.IsActivated = true;

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
