using Assets.Scripts.Services;
using Zenject;

namespace Assets.Scripts.GameInstallers
{
    //Начальная загрузка...
    //В этом классе мы инициализируем стартовое состояние для машины состояний. Загрузку.
    public class BootstrapInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            //Стартер, который инициализирует начальное состояние. 
            //-100 порядок инициализации. Будет инициализироваться самым первым.
            //Container.Bind<GameStarter>().AsSingle().NonLazy();
            Container.BindInitializableExecutionOrder<GameStarter>(-100);
            Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle().NonLazy();
        }
    }
}
