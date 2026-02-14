using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Services;
using Assets.Scripts.Utilites;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI._2_GamePanelWindow
{
    public class GameMenuPanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToGameButton;
        [SerializeField] private Button _backToMenuButton;

        private SignalBus _signalBus;
        private PauseController _pauseController;

        public bool IsVisible { get; private set; }

        #if UNITY_EDITOR
        public void OnValidate()
        {
            if(_backToGameButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToGameButton is not set!", this);
            }

            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
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
            new ButtonWithCommand<OnBackButtonGameClickedSignal>(_backToGameButton, _signalBus, new OnBackButtonGameClickedSignal());
            new ButtonWithCommand<OnExitButtonGameClickedSignal>(_backToMenuButton, _signalBus, new OnExitButtonGameClickedSignal());
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
            _pauseController.AllPause();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
            _pauseController.AllContinue();
        }
    }
}
