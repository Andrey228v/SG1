using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class SceneLoader
    {
        public void ChangeWindow(string window)
        {
            SceneManager.LoadScene(window);
        }
    }
}
