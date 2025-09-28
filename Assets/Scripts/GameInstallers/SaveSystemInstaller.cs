using Assets.Scripts.Services.Save;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameInstallers
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("SAVE LOAD INSTALLER");

            //// Сервис сохранений
            //Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

            //// Сигналы
            //Container.DeclareSignal<GameSavedSignal>();
            //Container.DeclareSignal<GameLoadedSignal>();
            //Container.DeclareSignal<CoinCollectedSignal>();
            //Container.DeclareSignal<CheckpointActivatedSignal>();
            //Container.DeclareSignal<PlayerDiedSignal>();
            //Container.DeclareSignal<PlayerRespawnedSignal>();

            // Фабрики для создаваемых объектов
            //Container.BindFactory<Coin, Coin.Factory>().FromComponentInNewPrefab(/* prefab reference */);
            //Container.BindFactory<Checkpoint, Checkpoint.Factory>().FromComponentInNewPrefab(/* prefab reference */);
        }
    }
}
