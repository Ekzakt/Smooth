using Microsoft.AspNetCore.Components;
using Smooth.Client.Flaunt.HttpClients;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using Smooth.Shared.Endpoints;
using System.Net.Http.Json;
using System.Text.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Home
{
    [Inject]
    public ApiHttpClient _httpClient { get; set; }


    private string _mediaFilesOptions = string.Empty;
    private string _apiVersions = string.Empty;
    private string _webVersions = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        await SetMediaFileOptionsAsync();
        await SetVersionAsync();
    }



    #region Helpers

    private async Task SetMediaFileOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.MEDIAFILES();

        var result = await _httpClient.Client.GetFromJsonAsync<MediaFilesOptions>(endpoint);

        if (result is not null)
        {
            _mediaFilesOptions = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });
        }
    }


    private async Task SetVersionAsync()
    {
        var endpoint = ConfigurationEndpoints.VERSIONS();

        var result = await _httpClient.Client.GetFromJsonAsync<Versions>(endpoint);

        if (result is not null)
        {
            var version = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });

            _apiVersions = version;
        }
    }

    #endregion Helpers
}
