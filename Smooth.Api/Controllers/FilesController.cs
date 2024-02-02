using Ekzakt.FileManager.Core.Contracts;
using Ekzakt.FileManager.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Smooth.Api.SignalR;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers;

[Route(Ctrls.FILES)]
[ApiController]
public class FilesController(
    IHubContext<NotificationsHub> hub,
    IFileManager fileMananager)
    : ControllerBase
{
    private readonly IHubContext<NotificationsHub> _hub = hub;
    private readonly IFileManager _fileManagerService = fileMananager;


    [HttpGet]
    [Route(Routes.GET_FILES_LIST)]
    public async Task<IActionResult> GetFilesList(CancellationToken cancellationToken)
    {
        var request = new ListFilesRequest
        {
            ContainerName = "demo-blazor8",
            CorrelationId = Guid.NewGuid()
        };


        var result = await _fileManagerService.ListFilesAsync(request, cancellationToken);

        return Ok(result.Data);

    }
}
