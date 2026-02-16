using Assets.Scripts.GameSM;
using Assets.Scripts.Utilites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save.CheckPoints
{
    public class CheckPointController : IInitializable, IDisposable, IAsyncService, IInitEvent
    {
        //private Dictionary<string, Checkpoint> _checkpointsDictionary;
        private List<Checkpoint> _checkPoints;
        private int _checkpointsCount;
        private SignalBus _signalBus;
        private Transform _checkPointsTransform;
        private ISaveLoadService _saveLoadService;

        public event Action OnInitComplite;

        [Inject]
        public CheckPointController(SignalBus signalBus, [Inject(Id = "CheckPoints")] Transform checkPointsTransform, ISaveLoadService saveLoadService)
        {
            _signalBus = signalBus;
            _checkPointsTransform = checkPointsTransform;
            _saveLoadService = saveLoadService;

            _signalBus.Subscribe<GameSavedSignal>(SaveAll);
            _signalBus.Subscribe<GameLoadedSignal>(LoadAll);
        }

        public void Initialize()
        {
            _checkPoints = new List<Checkpoint>();

            //Если сохранения не было мы должны создать новое, если есть то добавить...

            for (int i = 0; i < _checkPointsTransform.childCount; i++)
            {
                Checkpoint checkpoint = _checkPointsTransform.GetChild(i).GetComponent<Checkpoint>();
                _checkPoints.Add(checkpoint);
            }

            _checkpointsCount = _checkPoints.Count;
            OnInitComplite?.Invoke();
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

        private async void SaveAll()
        {
            //Здесь можно лучше придумать. Мы каждый раз всё с нуля перезаписываем... 
            //100% не имеет смысла. Пускай пока так будет.
            _saveLoadService.CurrentSave.checkpointsSaveData.checkpointsList = new List<CheckpointSaveData>();
            // _saveLoadService.CurrentSave.checkpointsSaveData.checkpointsList = _checkPoints[i];

            for (int i = 0; i < _checkpointsCount; i++)
            {
                CheckpointSaveData saveCheckpoint = _checkPoints[i].Save();
                _saveLoadService.CurrentSave.checkpointsSaveData.checkpointsList.Add(saveCheckpoint);
            }

            //await Task.Run(() => Test1());

        }

        private void LoadAll()
        {
            if (_saveLoadService.HasSave() && _saveLoadService.IsFirstLoad() == false) // ТУТ ОШИБКА ВЫЛЕТАЕТ ПУСТОЙ СПИСОК ПРИХОДИТ ПРИ ПЕРЕЗАПУСКЕ НАДО ПОДУМАТЬ....
            {
                for (int i = 0; i < _checkpointsCount; i++)
                {
                    CheckpointSaveData loadCheckpoint = _saveLoadService.CurrentSave.checkpointsSaveData.checkpointsList[i];
                    _checkPoints[i].Load(loadCheckpoint);
                }
            }
            else
            {

            }
        }


        //private void Test1()
        //{
        //    for (int j = 0; j < 5000; j++)
        //    {
        //        Debug.Log(j);
        //    }
        //}
    }
}
