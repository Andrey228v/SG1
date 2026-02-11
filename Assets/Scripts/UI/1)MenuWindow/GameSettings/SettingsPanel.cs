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

        private SignalBus _signalBus;

        public bool IsVisible {  get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            SetupButtons();
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if(_backButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backButton is not set!", this);
            }

            if(_deletProgressButton == null)
            {
                Debug.LogError($"{gameObject.name}: _deletProgressButton is not set!", this);
            }
        }
        #endif

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

        
    }
}
