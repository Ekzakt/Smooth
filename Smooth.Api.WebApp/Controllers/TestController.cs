using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Dtos;
using Ekzakt.Utilities.Helpers;
using Microsoft.AspNetCore.SignalR;
using Smooth.Api.WebApp.SignalR;
using Microsoft.AspNetCore.Authorization;
using Ekzakt.EmailSender.Contracts;
using Ekzakt.EmailSender.Models;

namespace Smooth.Api.WebApp.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TestController(
    IHubContext<NotificationsHub> hub,
    IEmailSenderService emailSenderService
    )
    : ControllerBase
{
    private readonly IHubContext<NotificationsHub> _hub = hub;
    private readonly IEmailSenderService _emailSenderService;


    [HttpPost]
    [Authorize]
    [Route("/testclass")]
    public async Task<IActionResult> InsertTestClass(InsertTestClassRequest request)
    {
        var output = await Task.Run(() =>
        {
            var result = IntHelpers.GetRandomPositiveInt();

            return result;
        });

        await _hub.Clients.All.SendAsync("ReceiveMessage", output);

        return Ok(new InsertTestClassResponse { Id = output });
    }


    [HttpPost]
    [Authorize]
    [Route("/triggeremail")]
    public async Task<IActionResult> SendEmail()
    {
        SendEmailRequest request = new();

        await _emailSenderService.SendAsync(request);

        return Created();
    }
}


