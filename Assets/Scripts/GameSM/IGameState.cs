using System.Threading.Tasks;

namespace Assets.Scripts.GameSM
{
    public interface IGameState
    {
        Task Enter();
        Task Exit();
    }

    public interface IGameStateWithPayload<T> : IGameState
    {
        Task Enter(T payload);
    }
}
