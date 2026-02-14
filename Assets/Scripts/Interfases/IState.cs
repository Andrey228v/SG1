using UnityEngine;

namespace Assets.Scripts.Interfases
{
    public interface IState
    {
        public void Enter();

        public void Exit();

        public void UpdateState();

        public void FixedUpdate();
    }
}
