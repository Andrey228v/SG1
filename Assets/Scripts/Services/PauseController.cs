using Assets.Scripts.Utilites;
using System.Collections.Generic;
using UnityEngine;
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
            //Time.timeScale = 0f;

            foreach (IPause pauseObject in _pauseObjectsList)
            {
                pauseObject.Pause();
            }
        }

        public void AllContinue()
        {

            //Time.timeScale = 1f;

            foreach (IPause pauseObject in _pauseObjectsList)
            {
                pauseObject.Continue();
            }
        }
    }
}
