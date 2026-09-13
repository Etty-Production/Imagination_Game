using Microsoft.EntityFrameworkCore;
using Test_LR1.Models;
using Test_LR1.Data;

namespace Test_LR1.Services
{
    public class PlayerRepositoryPostgress : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        public PlayerRepositoryPostgress(ApplicationDbContext context) => _context = context;

        public async Task<IReadOnlyList<Player>> GetFromDB(CancellationToken ct = default)
        {
            return await _context.Players.AsNoTracking().ToListAsync(ct);
        }

        public async Task PushDB(IReadOnlyList<Player> players, CancellationToken ct = default)
        {
            var existId = players.Select(p => p.UserId).ToList();
            var existPlayersDict = await  _context.Players.Where(p => existId.Contains(p.UserId)).ToDictionaryAsync(p => p.UserId);
            List<Player> ToAdd = new List<Player>();
            foreach (var rec in players)
            {
                if (existPlayersDict.TryGetValue(rec.UserId, out var dbPlayer))
                {
                    dbPlayer.Location_X = rec.Location_X;
                    dbPlayer.Location_Y = rec.Location_Y;
                    dbPlayer.HP = rec.HP;
                    dbPlayer.IsActive = rec.IsActive;
                } else
                {
                    ToAdd.Add(rec);
                }
            }
            if (ToAdd.Count > 0)
            {
               await _context.Players.AddRangeAsync(ToAdd);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeletePlayers(IReadOnlyList<string> playersId, CancellationToken ct = default)
        {
            var existPlayersDict = await _context.Players.Where(p => playersId.Contains(p.UserId)).ToListAsync();
            if (existPlayersDict.Count > 0)
            {
                foreach (var rec in existPlayersDict) _context.Players.Remove(rec);
                await _context.SaveChangesAsync();
            }

        }
    }
}
