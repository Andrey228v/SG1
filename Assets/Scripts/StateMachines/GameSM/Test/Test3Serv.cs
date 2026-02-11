using System;
using UnityEngine;

namespace Assets.Scripts.GameSM.Test
{
    public class Test3Serv : IAsyncService
    {
        public void AInitialize(Action onComplete)
        {
            Debug.Log("INIT TEST 3");
            onComplete.Invoke();
        }
    }
}
