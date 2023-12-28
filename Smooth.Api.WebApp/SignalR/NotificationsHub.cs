using Microsoft.AspNetCore.SignalR;

namespace Smooth.Api.WebApp.SignalR;

public class NotificationsHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
