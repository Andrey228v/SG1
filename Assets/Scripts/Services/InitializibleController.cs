using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Utilites;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Assets.Scripts.Services
{
    public class InitializibleController
    {
        private List<IInitEvent> _initializers;
        private int _counter;
        private SignalBus _signalBus;

        public InitializibleController(List<IInitEvent> initializers, SignalBus signalBus)
        {
            _counter = 0;
            _initializers = initializers;
            _signalBus = signalBus;

            Debug.Log($"IINIT COUNT: {_initializers.Count}");

            for (int i = 0; i < _initializers.Count; i++)
            {
                _initializers[i].OnInitComplite += AddCounter;
            }
        }

        private void AddCounter()
        {
            _counter++;

            if( _counter == _initializers.Count)
            {
                _signalBus.Fire(new OnGameInitialized());
            }
        }

    }
}
