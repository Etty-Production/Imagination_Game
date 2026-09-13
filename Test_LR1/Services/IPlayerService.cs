using System.Collections.Concurrent;
using Test_LR1.Models;

namespace Test_LR1.Services
{
    public interface IPlayerService
    {
        public bool RestoreFromDB(IReadOnlyCollection<Player> players);
        public bool AddPlayer(Player item);
        public Player? GetPlayer(string userId);
        public bool DeletePlayer(string userId);
        public IReadOnlyList<string> GetPlayersToDelete();
        public IReadOnlyList<Player> GetAllPlayers();
    }
}
