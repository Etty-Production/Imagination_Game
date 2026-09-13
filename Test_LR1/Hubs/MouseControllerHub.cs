using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Diagnostics;
using Test_LR1.Models;
using Test_LR1.Services;

namespace Test_LR1.Hubs
{
    public class MouseControllerHub : Hub
    {
        private IPlayerService _playerService;

        public MouseControllerHub(IPlayerService playerService)
        {
            _playerService = playerService; // ВЫ МОГЛИ ЗАБЫТЬ ЭТУ СТРОКУ!
        }
        public async Task SendMousePosition(string userId, double x, double y)
        {
            await Clients.All.SendAsync("ReceiveMousePosition", userId, x, y);
        }

        public async Task SendMouseClick(string userId, string action, double x, double y)
        {
            // action: "mousedown" или "mouseup"
            await Clients.All.SendAsync("ReceiveMouseClick", userId, action, x, y);
        }

        public void SendUserName(string userId)
        {
            
        }

        public override Task OnConnectedAsync()
        {
            string name = "TestDummy" + DateTime.Now.ToString();
            Clients.All.SendAsync("NewUserName", name);
            _playerService.AddPlayer(new Player(name ,100)); 
            return base.OnConnectedAsync();
        }
    }
}
