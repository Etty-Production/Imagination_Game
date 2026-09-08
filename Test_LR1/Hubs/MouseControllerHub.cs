using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Test_LR1.Hubs
{
    public class MouseControllerHub : Hub
    {
        public async Task SendMousePosition(string userId, double x, double y)
        {
            await Clients.All.SendAsync("ReceiveMousePosition", userId, x, y);
        }

        public async Task SendMouseClick(string userId, string action, double x, double y)
        {
            // action: "mousedown" или "mouseup"
            await Clients.All.SendAsync("ReceiveMouseClick", userId, action, x, y);
        }
    }
}
