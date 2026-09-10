using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Diagnostics;
using Test_LR1.Services;

namespace Test_LR1.Hubs
{
    public class MouseControllerHub : Hub
    {
        private PlayerController _playerController;
        public async Task SendMousePosition(string userId, double x, double y)
        {
            await Clients.All.SendAsync("ReceiveMousePosition", userId, x, y);
        }

        public async Task SendMouseClick(string userId, string action, double x, double y)
        {
            // action: "mousedown" или "mouseup"
            await Clients.All.SendAsync("ReceiveMouseClick", userId, action, x, y);
        }

        public override Task OnConnectedAsync()
        {
            //_playerController.AddPlayer()

            return base.OnConnectedAsync();
        }
    }
}
