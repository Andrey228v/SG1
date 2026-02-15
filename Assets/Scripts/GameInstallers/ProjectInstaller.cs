using Assets.Scripts;
using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.GameSM;
using Assets.Scripts.GameSM.States;
using Assets.Scripts.Services.Audio;
using Assets.Scripts.Services.Initializer;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Units;
using Assets.Scripts.Utilites;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AudioSettingsSO _audioSettings;
    [SerializeField] private AudioMixer _audioMixer;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 45;

        SignalBusInstaller.Install(Container);

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        Container.Bind<PlayerProgress>().AsSingle().NonLazy();
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();

        // Сервисы
        Container.BindInterfacesAndSelfTo<InitService>().AsSingle(); // test
        Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

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

        //Audio
        Container.BindInstance(_audioSettings).AsSingle();

        // Регистрируем AudioMixer (опционально)
        Container.BindInstance(_audioMixer).AsSingle();


        // Регистрируем AudioService как интерфейс
        Container.Bind<IAudioService>().To<AudioService>().AsSingle().NonLazy(); // Создастся сразу при старте

    }
}
