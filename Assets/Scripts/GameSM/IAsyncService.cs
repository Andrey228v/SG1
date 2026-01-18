using System;

namespace Assets.Scripts.GameSM
{
    public interface IAsyncService
    {
        public void AInitialize(Action onComplete);

    }
}