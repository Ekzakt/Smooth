using Microsoft.AspNetCore.Components;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using Smooth.Shared.Dtos;
using Smooth.Shared.Endpoints;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Home
{
    [Inject]
    public IHttpDataManager _httpDataManager { get; set; }

    [Inject]
    public NavigationManager _navigationMananger { get; set; }

    private string? _mediaFilesOptions = string.Empty;
    private string? _imageOptions = string.Empty;
    private string? _videoOptions = string.Empty;
    private string? _soundOptions = string.Empty;
    private string? _apiVersions = string.Empty;
    private string? _webVersions = string.Empty;
    private string? _baseAddress = string.Empty;
    private string? _insertTestClassResult = string.Empty;
    private string? _isWasm = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await SetOptionsAsync();
        await SetVersionsAsync();

        SetBaseAddress();
        SetIsWasm();
    }




    #region Helpers

    private async Task InsertTestClassAsync()
    {
        var result = await _httpDataManager.Insert<InsertTestClassResponsDto, InsertTestClassRequestDto>(TestEndpoints.INSERT_TEST_CLASS(), new InsertTestClassRequestDto
        {
            Name = "Test class name",
            Description = "Test class description"
        });

        _insertTestClassResult = result.Id.ToString();
    }


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

        _mediaFilesOptions = await _httpDataManager!.GetSerializedDataAsync<MediaFilesOptions>(endpoint);
    }


    private async Task SetImageOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.IMAGE_OPTIONS();

        _imageOptions = await _httpDataManager!.GetSerializedDataAsync<ImageOptions>(endpoint);
    }


    private async Task SetVideoOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.VIDEO_OPTIONS();

        _videoOptions = await _httpDataManager!.GetSerializedDataAsync<VideoOptions>(endpoint);
    }


    private async Task SetSoundOptionsAsync()
    {
        var endpoint = ConfigurationEndpoints.SOUND_OPTIONS();

        _soundOptions = await _httpDataManager!.GetSerializedDataAsync<SoundOptions>(endpoint);
    }


    private async Task SetVersionsAsync()
    {
        await SetApiVersionsAsync();
        SetWebVersions();
    }


    private async Task SetApiVersionsAsync()
    {
        var endpoint = ConfigurationEndpoints.APP_VERSIONS();

        _apiVersions = await _httpDataManager!.GetSerializedDataAsync<AppVersions>(endpoint);
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


    private void SetIsWasm()
    {
        var isWasm = RuntimeInformation.IsOSPlatform(OSPlatform.Create("WEBASSEMBLY"));

        _isWasm = isWasm.ToString();
    }


    private void SetBaseAddress()
    {
        _baseAddress = _navigationMananger.BaseUri;
    }


    #endregion Helpers
}
