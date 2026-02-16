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
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnSettingsSave>(SaveSettings);
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

        public SettingsSaveData LoadSettings()
        {
            string json = PlayerPrefs.GetString(SAVE_SETTINGS);
            return JsonUtility.FromJson<SettingsSaveData>(json);
        }

        public bool HasSave()
        {
            return PlayerPrefs.HasKey(SAVE_SETTINGS) == false;
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(SAVE_SETTINGS);
            CurrentSave = new SettingsSaveData();
            _isFirstLoad = true;
        }

    }
}
