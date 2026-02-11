using Assets.Scripts.GameInstallers.Signals;
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

        public bool IsVisible { get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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
