using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Container.BindInitializableExecutionOrder<GameStarter>(-100);
            //Container.Bind<GameStarter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle().NonLazy();
        }
    }
}
