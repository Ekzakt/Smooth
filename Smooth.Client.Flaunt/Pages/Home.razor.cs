using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.Options.Azure;
using Smooth.Shared.Configurations.Options.MediaFiles;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;
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
    private string? _insertTestClassResult = string.Empty;
    private string? _triggerEmailResult = string.Empty;
    private string? _azureOptons = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        await SetOptionsAsync();
        await SetVersionsAsync();
    }



    #region Helpers

    private async Task InsertTestClassAsync()
    {
        var result = await _httpDataManager.PostDataAsync<InsertTestClassResponse, InsertTestClassRequest>(EndPoints.INSERT_TESTCLASS(), new InsertTestClassRequest
        {
            Name = "Test class name",
            Description = "Test class description"
        }, true);

        _insertTestClassResult = result is null
            ? string.Empty
            : result.Id.ToString();
    }


    private async Task TriggerSendEmailAsync()
    {
        var result = await _httpDataManager.GetSerializedDataAsync<TriggerEmailResponse>(EndPoints.TRIGGER_EMAIL(), true);

        _triggerEmailResult = result;
    }


    private async Task SetOptionsAsync()
    {
        await SetMediaFileOptionsAsync();
        await SetImageOptionsAsync();
        await SetVideoOptionsAsync();
        await SetSoundOptionsAsync();
        await SetAzureOptionsAsync();
    }


    private async Task SetMediaFileOptionsAsync()
    {
        var endpoint = EndPoints.GET_MEDIAFILES_OPTIONS();

        _mediaFilesOptions = await _httpDataManager!.GetSerializedDataAsync<MediaFilesOptions>(endpoint, usePublicHttpClient: true);
    }


    private async Task SetImageOptionsAsync()
    {
        var endpoint = EndPoints.GET_IMAGE_OPTIONS();

        _imageOptions = await _httpDataManager!.GetSerializedDataAsync<ImageOptions>(endpoint, usePublicHttpClient: true);
    }


    private async Task SetVideoOptionsAsync()
    {
        var endpoint = EndPoints.GET_VIDEO_OPTIONS();

        _videoOptions = await _httpDataManager!.GetSerializedDataAsync<VideoOptions>(endpoint, usePublicHttpClient: true);
    }


    private async Task SetSoundOptionsAsync()
    {
        var endpoint = EndPoints.GET_SOUND_OPTIONS();

        _soundOptions = await _httpDataManager!.GetSerializedDataAsync<SoundOptions>(endpoint, usePublicHttpClient: true);
    }


    private async Task SetAzureOptionsAsync()
    {
        var endpoint = EndPoints.GET_AZURE_OPTIONS();

        _azureOptons = await _httpDataManager!.GetSerializedDataAsync<AzureOptions>(endpoint, usePublicHttpClient: true);
    }

    private async Task SetVersionsAsync()
    {
        await SetApiVersionsAsync();
        SetWebVersions();
    }


    private async Task SetApiVersionsAsync()
    {
        var endpoint = EndPoints.GET_APP_VERSIONS();

        _apiVersions = await _httpDataManager!.GetSerializedDataAsync<AppVersions>(endpoint, usePublicHttpClient: true);
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
