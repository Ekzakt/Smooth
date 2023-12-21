using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Endpoints;
using Smooth.Client.Flaunt.HttpClients;
using Smooth.Shared.MediaFiles.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Home
{
    [Inject]
    public ApiHttpClient _httpClient { get; set; }


    private string _mediaFilesOptions = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        await SetMediaFileOptionsAsync();        
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
                IgnoreReadOnlyFields = true,
                WriteIndented = true 
            });
        }
    }

    #endregion Helpers
}
