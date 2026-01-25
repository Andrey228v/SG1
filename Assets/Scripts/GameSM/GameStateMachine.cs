using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameSM
{

    //Версия №2 
    public class GameStateMachine
    {
        private readonly DiContainer _container;
        private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
        private IGameState _currentState;
        private bool _isTransitioning;

        //[Inject]
        public GameStateMachine(DiContainer container)
        {
            _container = container;
            Debug.Log("TEST....");

        }

        //Надо ли это ....
        public async Task Initialize()
        {
            Debug.Log("GameStateMachine: Инициализация...");

            // Регистрируем все состояния
            RegisterStates();

            Debug.Log("GameStateMachine: Инициализация завершена");
        }

        //Надо ли это ....
        private void RegisterStates()
        {
            // Все состояния уже зарегистрированы в контейнере
            // Здесь можно добавить дополнительную логику инициализации
        }

        public async Task Enter<TState>() where TState : IGameState
        {
            if (_isTransitioning)
            {
                Debug.LogWarning($"GameStateMachine: Попытка перехода во время другого перехода. Целевое состояние: {typeof(TState).Name}");
                return;
            }

            try
            {
                _isTransitioning = true;

                Debug.Log($"GameStateMachine: Начало перехода из {_currentState?.GetType().Name ?? "null"} в {typeof(TState).Name}");

                // Выход из текущего состояния
                if (_currentState != null)
                {
                    await _currentState.Exit();
                    Debug.Log($"GameStateMachine: Выход из {_currentState.GetType().Name}");
                }

                // Получение нового состояния
                var nextState = _container.Resolve<TState>();

                // Вход в новое состояние
                await nextState.Enter();
                _currentState = nextState;

                Debug.Log($"GameStateMachine: Переход в {typeof(TState).Name} завершен");
            }
            catch (Exception ex)
            {
                Debug.LogError($"GameStateMachine: Ошибка перехода в состояние {typeof(TState).Name}: {ex.Message}");
                throw;
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        public async Task Enter<TState, TPayload>(TPayload payload) where TState : IGameStateWithPayload<TPayload>
        {
            if (_isTransitioning)
            {
                Debug.LogWarning($"GameStateMachine: Попытка перехода во время другого перехода");
                return;
            }

            try
            {
                _isTransitioning = true;

                Debug.Log($"GameStateMachine: Начало перехода с payload в {typeof(TState).Name}");

                // Выход из текущего состояния
                if (_currentState != null)
                {
                    await _currentState.Exit();
                }

                // Получение нового состояния
                var nextState = _container.Resolve<TState>();

                // Вход в новое состояние с payload
                await nextState.Enter(payload);
                _currentState = nextState;

                Debug.Log($"GameStateMachine: Переход в {typeof(TState).Name} с payload завершен");
            }
            catch (Exception ex)
            {
                Debug.LogError($"GameStateMachine: Ошибка перехода: {ex.Message}");
                throw;
            }
            finally
            {
                _isTransitioning = false;
            }
        }

        public Type GetCurrentStateType()
        {
            return _currentState?.GetType();
        }

        public bool IsInState<TState>() where TState : IGameState
        {
            return _currentState is TState;
        }
    }
}
