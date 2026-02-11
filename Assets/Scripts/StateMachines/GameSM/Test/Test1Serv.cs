using System;
using UnityEngine;

namespace Assets.Scripts.GameSM.Test
{
    public class Test1Serv : IAsyncService
    {
        public void AInitialize(Action onComplete)
        {
            Debug.Log("INIT TEST 1");
            onComplete.Invoke();
        }
    }
}
