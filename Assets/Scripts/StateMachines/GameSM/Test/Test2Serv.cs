using System;
using UnityEngine;

namespace Assets.Scripts.GameSM.Test
{
    public class Test2Serv : IAsyncService
    {
        public void AInitialize(Action onComplete)
        {
            Debug.Log("INIT TEST 2");
            onComplete.Invoke();
        }
    }
}
