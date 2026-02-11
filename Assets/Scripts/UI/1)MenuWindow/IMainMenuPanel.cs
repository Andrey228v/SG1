namespace Assets.Scripts.UI._1_MenuWindow
{
    public interface IMainMenuPanel
    {
        public void Show();
        public void Hide();
        public void SetContinueButtonEnabled(bool enabled);
        public bool IsVisible { get; }
    }
}
