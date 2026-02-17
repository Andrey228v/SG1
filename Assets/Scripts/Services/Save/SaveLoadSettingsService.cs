using Assets.Scripts.GameInstallers.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class SaveLoadSettingsService : IInitializable, IDisposable
    {
        private const string SAVE_SETTINGS = "settings_save";

        private SignalBus _signalBus;
        private bool _isFirstLoad = true;

        public SettingsSaveData CurrentSave { get; private set; }

        public event Action OnGameSaved;
        public event Action OnGameLoaded;

        [Inject]
        public SaveLoadSettingsService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<OnSettingsSave>(SaveSettings);
            //_signalBus.Subscribe<OnGameInitialized>(Load);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnSettingsSave>(SaveSettings);
            //_signalBus.Unsubscribe<OnGameInitialized>(Load);
        }

        public void SaveSettings()
        {
            if (CurrentSave == null)
            {
                return;
            }

            _signalBus.Fire(new SettingsSavedSignal());

            string json = JsonUtility.ToJson(CurrentSave, true);

            PlayerPrefs.SetString(SAVE_SETTINGS, json);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            CurrentSave = LoadSettings();
        }

        public SettingsSaveData LoadSettings()
        {
            if (HasSave() == false)
            {
                return new SettingsSaveData();
            }

            string json = PlayerPrefs.GetString(SAVE_SETTINGS);
            return JsonUtility.FromJson<SettingsSaveData>(json);
        }

        public bool HasSave()
        {
            return PlayerPrefs.HasKey(SAVE_SETTINGS);
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SAVE_SETTINGS);
            CurrentSave = new SettingsSaveData();
            _isFirstLoad = true;
        }

    }
}
