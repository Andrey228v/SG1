using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameSM.States
{
    public class GameState : IGameState
    {
        public async Task Enter()
        {
            SceneManager.LoadScene(CONSTANTS.LEVEL1);
            Debug.Log("Начало уровня 1");
        }

        public async Task Exit()
        {
            
        }

        public async Task UpdateState()
        {
            
        }
    }
}
