using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Services;
using Assets.Scripts.Utilites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI._2_GamePanelWindow
{
    public class GameInterfacePanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _soundButton;

        [Header("Game data")]
        [SerializeField] private TextMeshProUGUI _coinsCounter;
        [SerializeField] private TextMeshProUGUI _timer;

        private SignalBus _signalBus;
        private PauseController _pauseController;

        public bool IsVisible { get; private set; }

        #if UNITY_EDITOR
        public void OnValidate()
        {
            if (_menuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _menuButton is not set!", this);
            }

            if (_loadButton == null)
            {
                Debug.LogError($"{gameObject.name}: _loadButton is not set!", this);
            }

            if (_soundButton == null)
            {
                Debug.LogError($"{gameObject.name}: _soundButton is not set!", this);
            }

            if (_coinsCounter == null)
            {
                Debug.LogError($"{gameObject.name}: _coinsCounter is not set!", this);
            }

            if (_timer == null)
            {
                Debug.LogError($"{gameObject.name}: _timer is not set!", this);
            }
        }
        #endif

        [Inject]
        public void Construct(SignalBus signalBus, PauseController pauseController)
        {
            _signalBus = signalBus;
            _pauseController = pauseController;
            SetupButtons();
        }

        private void SetupButtons()
        {
            new ButtonWithCommand<OnMenuInGameClickedSignal>(_menuButton, _signalBus, new OnMenuInGameClickedSignal());
            new ButtonWithCommand<OnMenuLoadGameClickedSignal>(_loadButton, _signalBus, new OnMenuLoadGameClickedSignal());
            new ButtonWithCommand<OnMenuSoundGameClickedSignal>(_soundButton, _signalBus, new OnMenuSoundGameClickedSignal());
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
    }
}
