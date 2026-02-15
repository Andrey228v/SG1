using Assets;
using Assets.Scripts;
using Assets.Scripts.Debugs;
using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Save.CheckPoints;
using Assets.Scripts.StateMachines.GameUISM;
using Assets.Scripts.StateMachines.GameUISM.Starter;
using Assets.Scripts.StateMachines.GameUISM.State;
using Assets.Scripts.StateMachineUnit;
using Assets.Scripts.UI._2_GamePanelWindow;
using Assets.Scripts.Units;
using Assets.Scripts.Units.States;
using ECM2;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Character _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private UnitDebug _unitDebug;
    [SerializeField] private Transform _checkPoints;
    [SerializeField] private GameInterfacePanel _gameInterfacePanel;
    [SerializeField] private GameMenuPanel _gameMenuPanel;
    [SerializeField] private InputReader _inputReader;

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

        Container.Bind<Character>()
           .FromComponentInNewPrefab(_playerPrefab)
           .AsSingle()
           .OnInstantiated<Character>((ctx, unit) =>
           {
               
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

        if (_inputReader != null) 
        {
            Container.BindInstance(_inputReader).AsSingle().NonLazy();
        }
        else
        {
            Debug.LogWarning("[SceneInstaller] _inputReader не назначен");
        }

    }

    private void InstallControllers()
    {
        Container.BindInitializableExecutionOrder<LevelStarter>(-100);
        Container.BindInterfacesAndSelfTo<LevelStarter>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameUIStarter>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameUIStateMachine>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameMenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameInterfaceState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GamePanelController>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<SignalReader>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UnitSignalReader>().AsSingle();
        Container.BindInterfacesAndSelfTo<UnitStateMachine>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FallState>().AsSingle();
        Container.BindInterfacesAndSelfTo<JumpState>().AsSingle();
        Container.BindInterfacesAndSelfTo<RunState>().AsSingle();
        Container.BindInterfacesAndSelfTo<StayState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerView>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnimatorPersonController>().AsSingle();

        Container.BindInterfacesAndSelfTo<CheckPointController>().AsSingle();
        Container.BindInstance(_cameraController).AsSingle();

        Container.Bind<InitializibleController>().AsSingle().NonLazy();
        Container.Bind<PauseController>().AsSingle().NonLazy();
    }

    private void InstallFactory()
    {
        //Container.BindFactory<Unit, Unit.Factory>()
        //    .FromComponentInNewPrefab(_playerPrefab)
        //    .WithGameObjectName("PlayerMain")
        //    .AsSingle();
    }
}
