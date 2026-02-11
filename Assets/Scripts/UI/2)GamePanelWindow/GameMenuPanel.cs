using Assets.Scripts.GameInstallers.Signals;
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

        public bool IsVisible { get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }
    }
}
