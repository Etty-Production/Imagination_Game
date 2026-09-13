using Microsoft.AspNetCore.DataProtection;
using System.Data;
using Test_LR1.Data;
using Test_LR1.Models;
using Test_LR1.Services;

namespace Test_LR1.Services
{
    public class PlayerFlushService : BackgroundService
    {
        private readonly IPlayerService _players;
        private readonly IServiceScopeFactory _scope;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

        public PlayerFlushService(IPlayerService playerService, 
            IServiceScopeFactory scope)
        {
            _players = playerService;
            _scope = scope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await RestoreAsync(stoppingToken);
            using var timer = new PeriodicTimer(_interval);
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    while(await timer.WaitForNextTickAsync(stoppingToken))
                        await Flush(stoppingToken);

                }
            } catch (OperationCanceledException)
            {

            }

        }

        private async Task RestoreAsync(CancellationToken stoppingToken)
        {
            try
            {
                var scope = _scope.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var repo = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
                IReadOnlyList<Player> players = await repo.GetFromDB(stoppingToken);
                _players.RestoreFromDB(players);
            }
            catch (Exception ex) { 
            
            }
        }

        private async Task Flush(CancellationToken stoppingToken)
        {
            var scope = _scope.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
            await repo.PushDB(_players.GetAllPlayers(), stoppingToken);
            await repo.DeletePlayers(_players.GetPlayersToDelete(), stoppingToken);
        }
    }
}
