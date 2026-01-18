using Assets;
using Assets.Scripts.Debugs;
using Assets.Scripts.GameSM.Test;
using Assets.Scripts.Services.Save.CheckPoints;
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

    public override void InstallBindings()
    {
        // TEST
        Container.BindInterfacesAndSelfTo<Test3Serv>().AsSingle(); // test

        Container.BindInterfacesAndSelfTo<LevelStarter>().AsSingle();
        Container.BindInterfacesAndSelfTo<CheckPointController>().AsSingle();

        Container.BindInstance(_cameraController).AsSingle();
        Container.Bind<Transform>().WithId("SpawnPoint").FromInstance(_playerSpawnPoint).AsCached();
        Container.BindInstance(_unitDebug).AsSingle();
        Container.Bind<Transform>().WithId("CheckPoints").FromInstance(_checkPoints).AsCached();

        Container.BindFactory<Unit, Unit.Factory>()
            .FromComponentInNewPrefab(_playerPrefab)
            .WithGameObjectName("PlayerMain")
            .AsSingle();

        Container.Bind<Unit>()
           .FromComponentInNewPrefab(_playerPrefab)
           .AsSingle()
           .OnInstantiated<Unit>((ctx, unit) => {
               unit.Initialize();
           })
           .NonLazy();
    }
}
