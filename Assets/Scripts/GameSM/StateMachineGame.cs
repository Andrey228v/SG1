using Assets.Scripts.Services.Save;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameSM
{
    public enum InitializationState
    {
        NotStarted,
        LoadingServices,
        Initialized,
        LoadingGameData,
        Ready
    }

    //Версия #1
    public class StateMachineGame : IInitializable, ITickable
    {
        private InitializationState _state = InitializationState.NotStarted;
        private readonly List<IAsyncService> _asyncServices;
        private readonly SaveLoadService _saveLoadService;
        private int _servicesLoaded = 0;

        public StateMachineGame(List<IAsyncService> asyncServices, SaveLoadService saveLoadService)
        {
            _asyncServices = asyncServices;
            _saveLoadService = saveLoadService;
            Debug.Log("TEST");
        }

        public void Initialize()
        {
            Debug.Log("TEST");
            _state = InitializationState.LoadingServices;
            StartServicesInitialization();
        }

        public void Tick()
        {
            switch (_state)
            {
                case InitializationState.LoadingServices:
                    CheckServicesProgress();
                    Debug.Log($"_servicesLoaded:{_servicesLoaded}");
                    break;
                case InitializationState.Initialized:
                    //Всё проинициализированно, игра ждёт действий.
                    break;
                case InitializationState.LoadingGameData:
                    // Можно добавить прогресс загрузки
                    break;
                case InitializationState.Ready:
                    Debug.Log($"READY");
                    break;
            }
        }

        public void AddService(IAsyncService serivice)
        {

        }

        private async void StartServicesInitialization()
        {
            foreach (var service in _asyncServices)
            {
                // Запускаем инициализацию сервиса
                service.AInitialize(OnServiceInitialized);
            }
        }

        private void OnServiceInitialized()
        {
            _servicesLoaded++;
            Debug.Log($"Загружен сервис {_servicesLoaded}/{_asyncServices.Count}");
        }

        private void CheckServicesProgress()
        {
            if (_servicesLoaded >= _asyncServices.Count)
            {
                Debug.Log("1)Игра полностью инициализирована!");
                _state = InitializationState.Initialized;
                //LoadGameData();
            }
        }

        private void LoadGameData()
        {
            // Все сервисы готовы, загружаем игру
            Debug.Log("2)ЗАГРУЖАЕМ.........");
            _saveLoadService.LoadGame();
            _state = InitializationState.Ready;
        }
    }
}
