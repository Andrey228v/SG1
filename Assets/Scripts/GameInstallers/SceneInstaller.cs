using Assets;
using Assets.Scripts;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerControllerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CameraController _cameraController;
    
    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<LevelStarter>().AsSingle();

        Container.BindInstance(_cameraController).AsSingle();
        Container.BindInstance(_playerSpawnPoint).AsSingle();

        Container.BindFactory<PlayerController, PlayerController.Factory>()
            .FromComponentInNewPrefab(_playerControllerPrefab)
            .WithGameObjectName("Player");



    }
}
