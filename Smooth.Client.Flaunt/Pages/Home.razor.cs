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
    private string _imageOptions = string.Empty;
    private string _videoOptions = string.Empty;
    private string _soundOptions = string.Empty;
    private string _apiVersions = string.Empty;
    private string _webVersions = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        await SetOptionsAsync();
        await SetVersionsAsync();
    }



    #region Helpers

    private async Task SetOptionsAsync()
    {
        await SetMediaFileOptionsAsync();
        await SetImageOptionsAsync();
        await SetVideoOptionsAsync();
        await SetSoundOptionsAsync();
    }


    private async Task SetMediaFileOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.MEDIAFILES_OPTIONS();

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


    private async Task SetImageOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.IMAGE_OPTIONS();

        var result = await _httpClient.Client.GetFromJsonAsync<ImageOptions>(endpoint);

        if (result is not null)
        {
            _imageOptions = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });
        }
    }


    private async Task SetVideoOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.VIDEO_OPTIONS();

        var result = await _httpClient.Client.GetFromJsonAsync<VideoOptions>(endpoint);

        if (result is not null)
        {
            _videoOptions = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });
        }
    }


    private async Task SetSoundOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.SOUND_OPTIONS();

        var result = await _httpClient.Client.GetFromJsonAsync<SoundOptions>(endpoint);

        if (result is not null)
        {
            _soundOptions = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreReadOnlyFields = false,
                WriteIndented = true
            });
        }
    }


    private async Task SetVersionsAsync()
    {
        await SetApiVersionsAsync();
        SetWebVersions();
    }


    private async Task SetApiVersionsAsync()
    {
        var endpoint = ConfigurationEndpoints.VERSIONS();

        var result = await _httpClient.Client.GetFromJsonAsync<AppVersions>(endpoint);

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


    private void SetWebVersions()
    {
        var result = new AppVersions(typeof(Program).Assembly.GetName()?.Version!, Environment.Version);

        var version = JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            IgnoreReadOnlyFields = false,
            WriteIndented = true
        });

        _webVersions = version;

    }

    #endregion Helpers
}
