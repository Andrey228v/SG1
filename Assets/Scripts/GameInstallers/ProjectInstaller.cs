using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.Test;
using Assets.Scripts.Services.Initializer;
using Assets.Scripts.Services.Save;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("PROJ INSTALLER");

        Application.targetFrameRate = 60;

        SignalBusInstaller.Install(Container);

        //TEST
        Container.BindInterfacesAndSelfTo<Test1Serv>().AsSingle(); // test
        Container.BindInterfacesAndSelfTo<Test2Serv>().AsSingle(); // test

        // Сервисы
        Container.BindInterfacesAndSelfTo<InitService>().AsSingle(); // test
        Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();
        //Container.BindInterfacesAndSelfTo<StateMachineGame>().AsSingle().CopyIntoAllSubContainers();
        Container.BindInterfacesAndSelfTo<StateMachineGame>().AsSingle();

        //// Сигналы
        Container.DeclareSignal<GameSavedSignal>();
        Container.DeclareSignal<GameLoadedSignal>();
        Container.DeclareSignal<CoinCollectedSignal>();
        Container.DeclareSignal<CheckpointActivatedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<PlayerRespawnedSignal>();
    }
}
