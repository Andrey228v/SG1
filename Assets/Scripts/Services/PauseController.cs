using Assets.Scripts.Utilites;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.Services
{
    public class PauseController
    {
        private List<IPause> _pauseObjectsList;

        [Inject]
        public PauseController(List<IPause> pauseObjectsList)
        {
            _pauseObjectsList = pauseObjectsList;
        }

        public void AllPause() 
        {
            foreach (IPause pauseObject in _pauseObjectsList)
            {
                pauseObject.Pause();
            }
        }

        public void AllContinue()
        {
            foreach (IPause pauseObject in _pauseObjectsList)
            {
                pauseObject.Continue();
            }
        }
    }
}
