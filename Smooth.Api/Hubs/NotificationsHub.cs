using Microsoft.AspNetCore.SignalR;

namespace Smooth.Api.Hubs;

public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }


    public async Task SendMessage(int id)
    {
        await Clients.All.SendAsync("ReceiveMessage", id);
    }
}
