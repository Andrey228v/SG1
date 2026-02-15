using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.UI._1_MenuWindow;
using Assets.Scripts.Utilites;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.GameSettings
{
    public class SettingsPanel : MonoBehaviour, IMainMenuPanel
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _deletProgressButton;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _uiSlider;
        [SerializeField] private Toggle _muteToggle;

        private SignalBus _signalBus;
        private IAudioService _audioService;

        public bool IsVisible {  get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus, IAudioService audioService)
        {
            _signalBus = signalBus;
            _audioService = audioService;
            SetupButtons();
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_backButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backButton is not set!", this);
            }

            if (_deletProgressButton == null)
            {
                Debug.LogError($"{gameObject.name}: _deletProgressButton is not set!", this);
            }

            if (_musicSlider == null)
            {
                Debug.LogError($"{gameObject.name}: _musicSlider is not set!", this);
            }

            if (_sfxSlider == null)
            {
                Debug.LogError($"{gameObject.name}: _sfxSlider is not set!", this);
            }

            if (_uiSlider == null)
            {
                Debug.LogError($"{gameObject.name}: _uiSlider is not set!", this);
            }

            if (_muteToggle == null)
            {
                Debug.LogError($"{gameObject.name}: _muteToggle is not set!", this);
            }
        }
        #endif

        private void Start()
        {
            // Инициализация слайдеров
            _musicSlider.value = _audioService.GetMusicVolume();
            _sfxSlider.value = _audioService.GetSFXVolume();
            _uiSlider.value = _audioService.GetUIVolume();

            // Подписка на события
            _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            _uiSlider.onValueChanged.AddListener(OnUIVolumeChanged);
            _muteToggle.onValueChanged.AddListener(OnMuteToggled);
            //closeButton.onClick.AddListener(CloseSettings);

            // Подписка на изменения громкости извне
            _audioService.OnMusicVolumeChanged += (volume) => _musicSlider.value = volume;
            _audioService.OnSFXVolumeChanged += (volume) => _sfxSlider.value = volume;
            _audioService.OnUIVolumeChanged += (volume) => _uiSlider.value = volume;
        }

        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
            _uiSlider.onValueChanged.RemoveListener(OnUIVolumeChanged);
            _muteToggle.onValueChanged.RemoveListener(OnMuteToggled);
        }

        private void SetupButtons()
        {
            new ButtonWithCommand<OnBackButtonClickedSignal>(_backButton, _signalBus, new OnBackButtonClickedSignal());
            new ButtonWithCommand<OnDeletProgressClickedSignal>(_deletProgressButton, _signalBus, new OnDeletProgressClickedSignal());
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }

        public void SetContinueButtonEnabled(bool enabled)
        {
            
        }

        private void OnMusicVolumeChanged(float value)
        {
            _audioService.SetMusicVolume(value);
            _audioService.PlayUISound(SoundType.ButtonClick); // Тестовый звук
        }

        private void OnSFXVolumeChanged(float value)
        {
            _audioService.SetSFXVolume(value);
            _audioService.PlaySound(SoundType.PlayerJump); // Тестовый звук
        }

        private void OnUIVolumeChanged(float value)
        {
            _audioService.SetUIVolume(value);
            _audioService.PlayUISound(SoundType.ButtonClick); // Тестовый звук
        }

        private void OnMuteToggled(bool isMuted)
        {
            _audioService.MuteAll(isMuted);
        }

        private void CloseSettings()
        {
            _audioService.PlayUISound(SoundType.MenuClose);
            gameObject.SetActive(false);
        }


    }
}
