using Assets.Scripts;
using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.States;
using Assets.Scripts.GameSM.Test;
using Assets.Scripts.Services.Initializer;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        SignalBusInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        Container.Bind<PlayerProgress>().AsSingle().NonLazy();
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        //Container.Bind<GameStateMachine>().AsSingle().NonLazy();

        //TEST
        Container.BindInterfacesAndSelfTo<Test1Serv>().AsSingle(); // test
        Container.BindInterfacesAndSelfTo<Test2Serv>().AsSingle(); // test

        // Сервисы
        Container.BindInterfacesAndSelfTo<InitService>().AsSingle(); // test
        Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

        //Container.BindInterfacesAndSelfTo<StateMachineGame>().AsSingle().CopyIntoAllSubContainers();
        //Container.BindInterfacesAndSelfTo<StateMachineGame>().AsSingle();

        Container.BindInterfacesAndSelfTo<LoadGameState>().AsSingle();
        Container.BindInterfacesAndSelfTo<MenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameState>().AsSingle();

        //// Сигналы
        Container.DeclareSignal<GameSavedSignal>();
        Container.DeclareSignal<GameLoadedSignal>();
        Container.DeclareSignal<CoinCollectedSignal>();
        Container.DeclareSignal<CheckpointActivatedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<PlayerRespawnedSignal>();
        Container.DeclareSignal<ApplicationFocusSignal>();
        Container.DeclareSignal<ApplicationPauseSignal>();
        Container.DeclareSignal<ApplicationQuitSignal>();
        Container.DeclareSignal<BootCompleteSignal>();
        Container.DeclareSignal<OnGameInitialized>();
        Container.DeclareSignal<OnGameLoaded>();
        Container.DeclareSignal<OnMenuLoadGameClickedSignal>();
        Container.DeclareSignal<OnCheckPointActivated>();
    }
}
