using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Dtos;
using Ekzakt.Utilities.Helpers;
using Microsoft.AspNetCore.SignalR;
using Smooth.Api.WebApp.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace Smooth.Api.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController
    : ControllerBase
{
    private readonly IHubContext<NotificationsHub> _hub;


    public TestController(IHubContext<NotificationsHub> hub)
    {
        _hub = hub;
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> InsertTestClass(InsertTestClassRequestDto request)
    {
        var output = await Task.Run(() =>
        {
            var result = IntHelpers.GetRandomPositiveInt();

            return result;
        });

        await _hub.Clients.All.SendAsync("ReceiveMessage", output);

        return Ok(new InsertTestClassResponsDto { Id = output });
    }
}


