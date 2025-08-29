using Assets.Scripts.Interfases;

namespace Assets.Scripts.Units.States
{
    public interface IStateUnit : IState<Unit>
    {
        public void CheckSwitchStates();

    }
}
