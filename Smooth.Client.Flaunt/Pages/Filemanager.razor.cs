using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Smooth.Client.Application.Hubs;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;
using Smooth.Shared.Models.HubMessages;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;
using System.Security.Cryptography;
using System.Text.Json;

namespace Smooth.Client.Flaunt.Pages;

public partial class Filemanager : IAsyncDisposable
{
    [Inject]
    private IHttpDataManager dataManager { get; set; }

    [Inject]
    private IFileManager fileManager { get; set; }

    [Inject]
    public ProgressHubService? _progressHubService { get; set; }


    private List<FileInformationDto>? filesList = null;
    private string saveFilesResult = string.Empty;


    protected override async Task OnInitializedAsync()
    {
        await _progressHubService!.StartAsync();

        _progressHubService.ProgressChanged += HandleProgressChanged;
    }




    #region Helpers

    private void HandleProgressChanged(ProgressHubMessage message)
    {
        saveFilesResult = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        StateHasChanged();
    }



    private async Task SaveFilesAsync(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            await fileManager.SaveFileAsync(file);

            await ListFilesAsync();
        }
    }


    private async Task DeleteFileAsync(string fileName)
    {
        var deleteRequest = new DeleteFileRequest { FileName = fileName };

        var result = await dataManager!.DeleteDataAsync<DeleteFileResponse>(EndPoints.DELETE_FILE(deleteRequest), true);

        if (result!.IsSuccess)
        {
            await ListFilesAsync();
        }
    }


    private async Task ListFilesAsync()
    {
        var result = await dataManager!.GetDataAsync<GetFilesListResponse>(EndPoints.GET_FILES_LIST(), true);

        if (result!.Files.Count > 0)
        {
            filesList = result.Files ?? new List<FileInformationDto>();
        }
    }


    public async ValueTask DisposeAsync()
    {
        if (_progressHubService is not null)
        {
            _progressHubService.ProgressChanged -= HandleProgressChanged;
            await _progressHubService!.StopAsync();
        }
    }


    #endregion Helpers
}
