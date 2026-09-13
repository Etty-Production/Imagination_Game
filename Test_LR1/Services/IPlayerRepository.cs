using Test_LR1.Models;

namespace Test_LR1.Services
{
    public interface IPlayerRepository
    {
        public Task<IReadOnlyList<Player>> GetFromDB(CancellationToken ct = default);
        public Task PushDB(IReadOnlyList<Player> players, CancellationToken ct = default);
        public Task DeletePlayers(IReadOnlyList<string> players, CancellationToken ct = default);
    }
}
