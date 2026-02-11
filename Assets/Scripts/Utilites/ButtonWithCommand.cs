using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Utilites
{
    public class ButtonWithCommand<TSignal>
    {

        public ButtonWithCommand(Button button, SignalBus signalBus, TSignal signal)
        {
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    signalBus.Fire(signal);
                });
            }
        }


    }
}
