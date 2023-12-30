using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
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
    private List<string> _messages = new();


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

    private async Task StartHubAsync()
    {
        if (!_startHub)
        {
            return;
        }

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

        await _hubConnection.StartAsync();
    }


    #endregion Helpers
}
