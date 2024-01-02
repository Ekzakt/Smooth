using Microsoft.AspNetCore.SignalR;

namespace Smooth.Api.WebApp.SignalR;

public class NotificationsHub : Hub
{
    public async Task SendMessage(int id)
    {
        await Clients.All.SendAsync("ReceiveMessage", id);
    }
}
