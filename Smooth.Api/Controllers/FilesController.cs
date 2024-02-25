using AutoMapper;
using Ekzakt.FileManager.Core.Contracts;
using Ekzakt.FileManager.Core.Models;
using Ekzakt.FileManager.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Smooth.Api.Hubs;
using Smooth.Api.Utilities;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;
using Smooth.Shared.Models.HubMessages;
using Smooth.Shared.Models.Responses;
using System.Net;
using System.Text.Json;
using System.Web;

namespace Smooth.Api.Controllers;

[Route(Ctrls.FILES)]
[ApiController]

public class FilesController(
    ILogger<FilesController> _logger,
    IMapper _mapper,
    IHubContext<NotificationsHub> _notificationsHub,
    IHubContext<ProgressHub> _progressHub,
    IFileManager _fileMananager)
    : ControllerBase
{
    [HttpGet]
    [Route(Routes.GET_FILES_LIST)]
    public async Task<IActionResult> GetFilesListAsync(CancellationToken cancellationToken)
    {
        var request = new ListFilesRequest
        {
            ContainerName = "demo-blazor8"
        };

        var result = await _fileMananager.ListFilesAsync(request, cancellationToken);

        var mapResult = _mapper.Map<List<FileInformationDto>>(result.Data);

        var output = new GetFilesListResponse { Files = mapResult };

        return result.IsSuccess()
            ? Ok(output)
            : StatusCode(StatusCodes.Status400BadRequest, result);
    }



    [HttpDelete]
    [Route(Routes.DELETE_FILE)]
    public async Task<IActionResult> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        fileName = HttpUtility.UrlDecode(fileName);

        var request = new Ekzakt.FileManager.Core.Models.Requests.DeleteFileRequest
        {
            ContainerName = "demo-blazor8",
            FileName = fileName
        };

        var result = await _fileMananager.DeleteFileAsync(request, cancellationToken);

        return Ok(new DeleteFileResponse { IsSuccess = result.IsSuccess() });
    }



    [HttpPost]
    [Route(Routes.POST_FILE)]
    public async Task<IActionResult> SaveFileAsync([FromForm] IFormFile file, Guid id, CancellationToken cancellationToken)
    {
        var fileGuid = Guid.NewGuid();

        using var fileStream = file.OpenReadStream();

        var request = new SaveFileRequest
        {
            ContainerName = "demo-blazor8",
            OriginalFilename = $"{fileGuid}.jpg",
            FileStream = fileStream,
            ProgressHandler = GetSaveFileProgressHandler(id),
            InitialFileSize = fileStream.Length
        };

        var result = await _fileMananager.SaveFileAsync(request, cancellationToken);

        if (result.IsSuccess())
        {
            return Ok(new PostFileResponse { FileId = fileGuid });
        }
        else
        {
            return BadRequest(result.Message);
        }
    }


    [HttpPost]
    [Route(Routes.POST_FILE_STREAM)]
    [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
    [MultipartFormData]
    [DisableFormValueModelBinding]
    public async Task<ActionResult<HttpResponseMessage>> SaveFileStreamAsync(CancellationToken cancellationToken)
    {
        var httpRequest = HttpContext.Request;
        var contentType = httpRequest?.ContentType;

        if (httpRequest is null || contentType is null)
        {
            throw new InvalidDataException($"{nameof(httpRequest.ContentType)} is null.");
        }

        var boundary = GetMultipartBoundary(MediaTypeHeaderValue.Parse(contentType));
        var multipartReader = new MultipartReader(boundary, httpRequest.Body);
        var fileGuid = Guid.NewGuid();
        var saveFileRequest = new SaveFileRequest();

        var section = await multipartReader.ReadNextSectionAsync();

        while (section != null)
        {
            var contentDisposition = section.GetContentDispositionHeader();

            if (contentDisposition!.IsFormDisposition())
            {
                var jsonString = section.ReadAsStringAsync(cancellationToken);
                var jsonContent = JsonSerializer.Deserialize<SaveFileFormDisposition>(jsonString.Result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                saveFileRequest.ContainerName = "demo-blazor8";
                saveFileRequest.ContentType = jsonContent.FileContentType;
                saveFileRequest.InitialFileSize = jsonContent.InitialFileSize;
            }
            else if (contentDisposition!.IsFileDisposition())
            {
                saveFileRequest.OriginalFilename = contentDisposition!.FileName.Value ?? string.Empty;
                saveFileRequest.FileStream = section.Body;
                saveFileRequest.ProgressHandler = GetSaveFileProgressHandler(Guid.NewGuid());

                var result = await _fileMananager.SaveFileAsync(saveFileRequest, cancellationToken);
            }

            section = await multipartReader.ReadNextSectionAsync();
        }

        return new HttpResponseMessage(HttpStatusCode.OK);
    }




    #region Helpers

    internal Progress<ProgressEventArgs> GetSaveFileProgressHandler(Guid progressId)
    {
        var progressHandler = new Progress<ProgressEventArgs>(async progress =>
        {
            await _progressHub.Clients.All.SendAsync("ProgressUpdated", new ProgressHubMessage
            {
                PercentageDone = progress.PercentageDone,
                ProgressId = progressId
            });
        });

        return progressHandler;
    }


    private string GetMultipartBoundary(MediaTypeHeaderValue contentType)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;

        if (string.IsNullOrWhiteSpace(boundary))
        {
            throw new InvalidDataException("Missing content-type boundary.");
        }

        return boundary;
    }

    #endregion Helpers
}