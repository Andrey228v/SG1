using Assets;
using Assets.Scripts.Debugs;
using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.GameSM.Test;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Save.CheckPoints;
using Assets.Scripts.StateMachines.GameUISM;
using Assets.Scripts.StateMachines.GameUISM.Starter;
using Assets.Scripts.StateMachines.GameUISM.State;
using Assets.Scripts.UI._2_GamePanelWindow;
using Assets.Scripts.Units;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Unit _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private UnitDebug _unitDebug;
    [SerializeField] private Transform _checkPoints;
    [SerializeField] private GameInterfacePanel _gameInterfacePanel;
    [SerializeField] private GameMenuPanel _gameMenuPanel;

    public override void InstallBindings()
    {
        InstallSettings();
        InstallSignals();
        InstallUIPanels();
        InstallControllers();
        InstallFactory();
    }

    private void InstallSettings()
    {
        Container.Bind<Transform>().WithId("SpawnPoint").FromInstance(_playerSpawnPoint).AsCached();
        Container.Bind<Transform>().WithId("CheckPoints").FromInstance(_checkPoints).AsCached();

        Container.Bind<Unit>()
           .FromComponentInNewPrefab(_playerPrefab)
           .AsSingle()
           .OnInstantiated<Unit>((ctx, unit) => {
               unit.Initialize();
           })
           .NonLazy();

    }

    private void InstallSignals()
    {
        Container.DeclareSignal<OnMenuInGameClickedSignal>();
        //Container.DeclareSignal<OnMenuLoadGameClickedSignal>();
        Container.DeclareSignal<OnMenuSoundGameClickedSignal>();
        Container.DeclareSignal<OnBackButtonGameClickedSignal>();
        Container.DeclareSignal<OnExitButtonGameClickedSignal>();
    }

    private void InstallUIPanels()
    {
        Container.BindInstance(_unitDebug).AsSingle();

        if (_gameInterfacePanel != null)
        {
            Container.Bind<GameInterfacePanel>()
                .FromInstance(_gameInterfacePanel)
                .AsSingle()
                .NonLazy();
        }
        else
        {
            Debug.LogWarning("[SceneInstaller] _gameInterfacePanel prefab не назначен");
        }

        if (_gameMenuPanel != null)
        {
            Container.Bind<GameMenuPanel>()
                .FromInstance(_gameMenuPanel)
                .AsSingle()
                .NonLazy();
        }
        else
        {
            Debug.LogWarning("[SceneInstaller] _gameMenuPanel prefab не назначен");
        }

    }

    private void InstallControllers()
    {
        Container.BindInitializableExecutionOrder<LevelStarter>(-100);
        Container.BindInterfacesAndSelfTo<LevelStarter>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameUIStarter>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameUIStateMachine>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameMenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameInterfaceState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GamePanelController>().AsSingle().NonLazy();


        Container.BindInterfacesAndSelfTo<CheckPointController>().AsSingle();
        Container.BindInstance(_cameraController).AsSingle();

        Container.Bind<InitializibleController>().AsSingle().NonLazy();
        Container.Bind<PauseController>().AsSingle().NonLazy();
    }

    private void InstallFactory()
    {
        Container.BindFactory<Unit, Unit.Factory>()
            .FromComponentInNewPrefab(_playerPrefab)
            .WithGameObjectName("PlayerMain")
            .AsSingle();
    }
}
