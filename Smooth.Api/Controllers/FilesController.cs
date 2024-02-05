using AutoMapper;
using Ekzakt.FileManager.Core.Contracts;
using Ekzakt.FileManager.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Smooth.Api.SignalR;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models;
using Smooth.Shared.Models.Responses;
using System.Web;

namespace Smooth.Api.Controllers;

[Route(Ctrls.FILES)]
[ApiController]
public class FilesController(
    IMapper _mapper,
    IHubContext<NotificationsHub> _hub,
    IFileManager _fileMananager)
    : ControllerBase
{

    [HttpGet]
    [Route(Routes.GET_FILES_LIST)]
    public async Task<IActionResult> GetFilesList(CancellationToken cancellationToken)
    {
        var request = new ListFilesRequest
        {
            // TODO: Fix CorrelationId thing!
            ContainerName = "demo-blazor8",
            CorrelationId = Guid.NewGuid()  
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
    public async Task<IActionResult> DeleteFile(string fileName, CancellationToken cancellationToken)
    {
        fileName = HttpUtility.UrlDecode(fileName);

        var request = new DeleteFileRequest
        { 
            ContainerName = "demo-blazor8",
            FileName = fileName,
            // TODO: Fix CorrelationId thing!
            CorrelationId = Guid.NewGuid()
            
        };

        var result = await _fileMananager.DeleteFileAsync(request, cancellationToken);

        return Ok(new DeleteFileResponse { IsSuccess = result.IsSuccess() });
    }
}
