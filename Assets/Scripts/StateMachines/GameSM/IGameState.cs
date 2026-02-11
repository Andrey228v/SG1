using System.Threading.Tasks;

namespace Assets.Scripts.GameSM
{
    public interface IGameState
    {
        Task Enter();
        Task Exit();
        Task UpdateState();
    }
}
