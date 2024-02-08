using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Smooth.Client.Application.Managers;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;

namespace Smooth.Client.Flaunt.Pages;

public partial class Filemanager
{
    [Inject]
    private IHttpDataManager dataManager { get; set; }

    [Inject]
    private IFileManager fileManager { get; set; }


    private List<FileInformationDto>? filesList = null;
    private string saveFilesResult = string.Empty;




    #region Helpers

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

    #endregion Helpers
}
