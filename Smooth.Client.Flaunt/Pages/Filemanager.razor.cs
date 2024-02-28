using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Smooth.Client.Application.Hubs;
using Smooth.Client.Application.Managers;
using Smooth.Client.Flaunt.Components.ProgressBar;
using Smooth.Client.Flaunt.Configuration;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;
using Smooth.Shared.Models.HubMessages;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;
using System.Globalization;
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
    private bool cancelDisabled = true;
    private double percentageDone;

    private CancellationTokenSource? cancellationTokenSource;


    protected override async Task OnInitializedAsync()
    {
        await _progressHubService!.StartAsync();

        _progressHubService.ProgressChanged += HandleProgressChanged;
    }




    #region Helpers

    private void HandleProgressChanged(ProgressHubMessage message)
    {
        percentageDone = message.PercentageDone;

        saveFilesResult = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        StateHasChanged();
    }


    private async Task SaveFilesAsync(InputFileChangeEventArgs e)
    {
        using var cts = cancellationTokenSource = new();

        cancelDisabled = false;
        
        cts.Token.Register(() =>
        {
            saveFilesResult = "Operation cancelled.";
            cancelDisabled = true;
            StateHasChanged();
        });


        foreach (var file in e.GetMultipleFiles())
        {
            await fileManager.SaveFileStreamAsync(file, Guid.NewGuid(), cancellationTokenSource.Token);

            await ListFilesAsync();
        }

        cancelDisabled = true;

        await InvokeAsync(StateHasChanged);
    }


    private void CancelSaveFile()
    {
        cancellationTokenSource?.Cancel();
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

        filesList = result?.Files ?? new List<FileInformationDto>();
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
