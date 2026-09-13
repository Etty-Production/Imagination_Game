using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Numerics;
using Test_LR1.Data;
using Test_LR1.Models;

namespace Test_LR1.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly ConcurrentDictionary<string, Player> _playerList = new();
        private readonly ConcurrentQueue<string> _deletedPlayers = new();

        public bool AddPlayer(Player item)
        {
            if (item is null || !Player.TestUserId(item.UserId)) return false;
            return _playerList.TryAdd(item.UserId, item.Clone());        
        }

        public Player? GetPlayer(string userId)
        {
            if(Player.TestUserId(userId) && _playerList.TryGetValue(userId, out var buff)) return buff.Clone(); 
            return null;
        }

        public bool DeletePlayer(string userId)
        {
            if(!Player.TestUserId(userId)) return false;
            if (_playerList.TryRemove(userId, out _))
            {
                _deletedPlayers.Enqueue(userId);
                return true;
            }
            return false;    
        }

        public IReadOnlyList<Player> GetAllPlayers()
        {
            var snapshot = _playerList.Values.ToArray();
            return snapshot.Select(x => x.Clone()).ToList();
        }

        public IReadOnlyList<string> GetPlayersToDelete()
        {
            return _deletedPlayers.ToImmutableArray();
        }

        public bool RestoreFromDB(IReadOnlyCollection<Player> players)
        {
            bool restoreHealthy = true;
            foreach (var player in players)
            {
                restoreHealthy = (restoreHealthy && AddPlayer(player.Clone()));
            }
            return restoreHealthy;
        }
    }
}