using Assets.Scripts.Utilites;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameSM.States
{
    public class GameState : IGameState
    {
        private IAudioService _audioService;

        public GameState(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public async Task Enter()
        {
            SceneManager.LoadScene(CONSTANTS.LEVEL1);
            _audioService.PlayMusic(SoundType.GameMusic);
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
