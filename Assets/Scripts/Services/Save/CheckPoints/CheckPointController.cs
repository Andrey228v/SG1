using Assets.Scripts.GameSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save.CheckPoints
{
    public class CheckPointController : IInitializable, IDisposable, IAsyncService
    {
        private Dictionary<string, Checkpoint> _checkpointsDictionary;
        private SignalBus _signalBus;
        private Transform _checkPoints;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public CheckPointController(SignalBus signalBus, [Inject(Id = "CheckPoints")] Transform checkPoints, ISaveLoadService saveLoadService)
        {
            _signalBus = signalBus;
            _checkPoints = checkPoints;
            _saveLoadService = saveLoadService;

            _signalBus.Subscribe<GameSavedSignal>(SaveAll);
            _signalBus.Subscribe<GameLoadedSignal>(LoadAll);
        }

        public void Initialize()
        {
            Debug.Log($"INIT CHECK POINT CONTROLLER");

            _checkpointsDictionary = new Dictionary<string, Checkpoint>();

            //Если сохранения не было мы должны создать новое, если есть то добавить...

            for (int i = 0; i < _checkPoints.childCount; i++)
            {
                Checkpoint checkpoint = _checkPoints.GetChild(i).GetComponent<Checkpoint>();
                _checkpointsDictionary.Add(checkpoint.CheckpointId, checkpoint); // Тут вопрос так ли это делается....
            }

            //_saveLoadService.CurrentSave.CheckpointData.CheckpointsDictionary = _checkpointsDictionary;
        }

        public void AInitialize(Action onComplete)
        {
            Debug.Log("TEST ASYNC............");
        }

        public void Dispose()
        {
            Debug.Log($"TEST DISPOSE...");

            _signalBus.Unsubscribe<GameSavedSignal>(SaveAll);
            _signalBus.Unsubscribe<GameLoadedSignal>(LoadAll);
        }

        private void SaveAll()
        {
            Dictionary<string, Checkpoint> currentSave = _saveLoadService.CurrentSave.CheckpointData.CheckpointsDictionary;

            foreach (string key in currentSave.Keys)
            {
                currentSave[key] = _checkpointsDictionary[key];
            }

            //_saveLoadService.CurrentSave.CheckpointData.CheckpointsDictionary = _checkpointsDictionary;
        }

        private void LoadAll()
        {
            Dictionary<string, Checkpoint> currentSave = _saveLoadService.CurrentSave.CheckpointData.CheckpointsDictionary;

            foreach(Checkpoint checkpoint in currentSave.Values)
            {
                _checkpointsDictionary[checkpoint.CheckpointId].Load(checkpoint); // Выгялидит так себе, но пускай так пока будет...
            }
        }


    }
}
