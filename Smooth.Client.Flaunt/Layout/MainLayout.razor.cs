using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Smooth.Client.Flaunt.Configuration;
using Smooth.Shared;
using Smooth.Shared.Configurations;
using Smooth.Shared.Endpoints;

namespace Smooth.Client.Flaunt.Layout;

public partial class MainLayout : IAsyncDisposable
{
    [Inject]
    public NavigationManager? _navigationMananger { get; set; }


    [Inject]
    public IConfiguration? Configuration { get; set; }


    private AppVersions? _appVersions;
    private HubConnection? _hubConnection;
    private bool _startHub = true;
    private int _id = default;


    protected override async Task OnInitializedAsync()
    {
        var assemblyVersion = typeof(Program).Assembly?.GetName()?.Version;
        _appVersions = new AppVersions(assemblyVersion!, Environment.Version);

        ReadQueryStringValues();

        await StartHubAsync();
    }


    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }



    #region Helpers

    private async Task StartHubAsync()
    {
        if (!_startHub)
        {
            return;
        }

        var url = GetHubConnectionUrl();

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url, options => 
            {
                //options.HttpMessageHandlerFactory = innerHandler => new IncludeRequestCredentialsMessagHandler { InnerHandler = innerHandler };
                //options.Transports = HttpTransportType.WebSockets;
            })
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<int>("ReceiveMessage", (id) =>
        {
            _id = id;

            InvokeAsync(StateHasChanged); 
        });

        await _hubConnection.StartAsync();
    }


    private void ReadQueryStringValues()
    {
        var uri = _navigationMananger?.ToAbsoluteUri(_navigationMananger.Uri);
        var queryStrings = QueryHelpers.ParseQuery(uri?.Query);

        if (queryStrings.TryGetValue("sh", out var startHub))
        {
            if (bool.TryParse(startHub, out bool result))
            {
                _startHub = result;
            }
        }
    }


    private string GetHubConnectionUrl()
    {
        var output = Configuration?
           .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        output ??= _navigationMananger?.BaseUri.TrimEnd('/');

        output = $"{output}{SignalREndpoints.NOTIFICATIONS_HUB}";

        return output;
    }
    

    #endregion Helpers
}
