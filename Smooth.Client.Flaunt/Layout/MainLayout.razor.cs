using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
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
    private List<string> _messages = new();


    protected override async Task OnInitializedAsync()
    {
        var assemblyVersion = typeof(Program).Assembly?.GetName()?.Version;
        _appVersions = new AppVersions(assemblyVersion!, Environment.Version);

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
        var apiBaseAddress = Configuration?
           .GetValue<string>(Constants.API_BASE_ADDRESS_CONFIG_NAME);

        apiBaseAddress ??= _navigationMananger?.BaseUri.TrimEnd('/');

        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{apiBaseAddress}{SignalREndpoints.NOTIFICATIONS_HUB}")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            var encodedMsg = $"{user}: {message}";
            _messages.Add(encodedMsg);

            InvokeAsync(StateHasChanged);
        });

        //await _hubConnection.StartAsync();
    }


    #endregion Helpers
}
