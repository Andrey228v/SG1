using UnityEngine;

namespace Assets.Scripts.Interfases
{
    public interface IState<T> where T : Component
    {
        public void Enter();

        public void Exit();

        public void UpdateState();

        public void FixedUpdate();
    }
}
