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
