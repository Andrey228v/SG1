using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.StateMachines.MenuSM;
using Assets.Scripts.StateMachines.MenuSM.Starter;
using Assets.Scripts.StateMachines.MenuSM.States;
using Assets.Scripts.UI.GameSettings;
using Assets.Scripts.UI.Load;
using Assets.Scripts.UI.Menu;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameInstallers
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("Основные элементы UI")]
        [SerializeField] private MainMenuPanel _mainMenuPanelPrefab;
        [SerializeField] private SettingsPanel _settingsPanelPrefab;
        [SerializeField] private LoadingScreen _loadingScreenPrefab;

        [Header("Конфигурация")]
        [SerializeField] private bool _enableContinueButton = false;

        public override void InstallBindings() 
        {
            Container.BindInterfacesAndSelfTo<MenuStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuWindowState>().AsSingle();
            Container.BindInterfacesAndSelfTo<NewGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ContinueGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExitState>().AsSingle();

            Container.BindInitializableExecutionOrder<MenuStarter>(-100);
            Container.BindInterfacesAndSelfTo<MenuStarter>().AsSingle().NonLazy();

            InstallSettings();
            InstallSignals();
            InstallControllers();
            InstallUIPanels();
        }

        private void InstallSettings()
        {
            Container.Bind<bool>().WithId("EnableContinueButton").FromInstance(_enableContinueButton);
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<OnStartButtonClickedSignal>();
            Container.DeclareSignal<OnContinueButtonClickedSignal>();
            Container.DeclareSignal<OnSettingsButtonClickedSignal>();
            Container.DeclareSignal<OnExitButtonClickedSignal>();
            Container.DeclareSignal<OnMenuEnterSignal>();
            Container.DeclareSignal<OnBackButtonClickedSignal>();
            Container.DeclareSignal<OnDeletProgressClickedSignal>();
        }

        private void InstallUIPanels()
        {
            if (_mainMenuPanelPrefab != null)
            {
                Container.Bind<MainMenuPanel>()
                    .FromInstance(_mainMenuPanelPrefab)
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Debug.LogWarning("[MenuInstaller] MainMenuPanel prefab не назначен");
            }

            if (_settingsPanelPrefab != null)
            {
                Container.Bind<SettingsPanel>()
                    .FromInstance(_settingsPanelPrefab)
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Debug.LogWarning("[MenuInstaller] SettingsPanel prefab не назначен");
            }

            if (_loadingScreenPrefab != null)
            {
                Container.Bind<LoadingScreen>()
                    .FromInstance(_loadingScreenPrefab)
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Debug.LogWarning("[MenuInstaller] LoadingScreen prefab не назначен");
            }
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Debug>().AsSingle().NonLazy();
        }
    }
}
