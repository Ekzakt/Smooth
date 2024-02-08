using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Smooth.Client.Application.Hubs;

public abstract class AbstractHubService
{
    public HubConnection HubConnection { get; set; }

    private readonly NavigationManager? _navigationManager;
    private readonly IConfiguration? _configuration;

    protected abstract string HubEndpoint { get; }


    public AbstractHubService(
        IConfiguration configuration,
        NavigationManager navigationManager)
    {
        _configuration = configuration;
        _navigationManager = navigationManager;

        HubConnection = new HubConnectionBuilder()
           .WithUrl(GetHubConnectionUrl(), options =>
           {
               options.Transports = HttpTransportType.WebSockets;
           })
           .WithAutomaticReconnect()
           .Build();
    }



    public async Task StartAsync()
    {
        await HubConnection.StartAsync();
    }


    public async Task StopAsync()
    {
        await HubConnection.DisposeAsync();
    }




    #region Helpers

    private string GetHubConnectionUrl()
    {
        var output = _configuration?
           .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        output ??= _navigationManager?.BaseUri.TrimEnd('/');

        output = $"{output}{HubEndpoint}";

        return output;
    }

    #endregion Helpers
}
