using System.Collections.Concurrent;
using Test_LR1.Models;

namespace Test_LR1.Services
{
    public class PlayerController
    {

        private readonly ConcurrentDictionary<string, Player> _playerList = new();

        public Task<bool> AddPlayer(Player item, CancellationToken ct = default)
        {
            if (item != null) return Task.FromResult(_playerList.TryAdd(item.UserId, item));        
            return Task.FromResult(false);
        }

        public Task<Player?> GetPlayer(string userId, CancellationToken ct = default)
        {
            if(Player.TestUserId(userId) && _playerList.TryGetValue(userId, out var buff)) { 
                return Task.FromResult<Player?>(buff); 
            }
            return Task.FromResult<Player?>(null);
        }

        public Task<bool> DeletePlayer(string userId, CancellationToken ct = default)
        {
            if(!Player.TestUserId(userId)) return Task.FromResult(false);
            return Task.FromResult(_playerList.TryRemove(userId, out _));
           
        }
    }
}