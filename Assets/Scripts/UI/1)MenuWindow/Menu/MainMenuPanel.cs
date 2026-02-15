using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.UI._1_MenuWindow;
using Assets.Scripts.Utilites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenuPanel : MonoBehaviour, IMainMenuPanel
    {
        //[Header("Основные элементы")]
        //[SerializeField] private CanvasGroup _canvasGroup;
        //[SerializeField] private RectTransform _panelTransform;

        [Header("Текст")]
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _versionText;

        [Header("Кнопки")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;

        private SignalBus _signalBus;
        private IAudioService _audioService;

        public bool IsVisible { get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus, IAudioService audioService)
        {
            _signalBus = signalBus;
            _audioService = audioService;
            SetupButtons();
        }

        private void Start()
        {
            _audioService.PlayMusic(SoundType.MenuMusic);
        }

        private void SetupButtons() 
        {
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(() =>
                {
                    _signalBus.Fire(new OnStartButtonClickedSignal());
                    _audioService.PlayUISound(SoundType.ButtonClick);
                });
            }

            if (_continueButton != null)
            {
                _continueButton.onClick.AddListener(() =>
                {
                    _signalBus.Fire(new OnContinueButtonClickedSignal());
                    _audioService.PlayUISound(SoundType.ButtonClick);
                });
            }

            if (_settingsButton != null)
            {
                _settingsButton.onClick.AddListener(() =>
                {
                    _signalBus.Fire(new OnSettingsButtonClickedSignal());
                    _audioService.PlayUISound(SoundType.ButtonClick);
                });
            }

            if (_exitButton != null)
            {
                _exitButton.onClick.AddListener(() =>
                {
                    _signalBus.Fire(new OnExitButtonClickedSignal());
                    _audioService.PlayUISound(SoundType.ButtonClick);
                });
            }
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
            if (_continueButton != null)
            {
                _continueButton.interactable = enabled;

                // Визуальная индикация
                var colors = _continueButton.colors;
                colors.disabledColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
                _continueButton.colors = colors;
            }
        }


    }
}
