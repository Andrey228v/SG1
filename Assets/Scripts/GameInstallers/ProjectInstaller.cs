using Assets.Scripts.Services.Save;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("PROJ INSTALLER");

        SignalBusInstaller.Install(Container);

        // Сервис сохранений
        Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();


        //// Сигналы
        Container.DeclareSignal<GameSavedSignal>();
        Container.DeclareSignal<GameLoadedSignal>();
        Container.DeclareSignal<CoinCollectedSignal>();
        Container.DeclareSignal<CheckpointActivatedSignal>();
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<PlayerRespawnedSignal>();

    }

}
