using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Dtos;
using Ekzakt.Utilities.Helpers;
using Microsoft.AspNetCore.SignalR;
using Ekzakt.EmailSender.Core.Contracts;
using Ekzakt.EmailSender.Core.Models;
using Smooth.Shared.Endpoints;
using Smooth.Api.SignalR;

namespace Smooth.Api.Controllers;


[Route(Ctrls.TEST)]
[ApiController]
public class TestController(
    IHubContext<NotificationsHub> _hub,
    IEmailSenderService _emailSenderService,
    ILogger<TestController> _logger)
    : ControllerBase
{

    [HttpPost]
    [Route(Routes.INSERT_TESTCLASS)]
    public async Task<IActionResult> InsertTestClassAsync(InsertTestClassRequest request)
    {
        var output = await Task.Run(() =>
        {
            var result = IntHelpers.GetRandomPositiveInt();

            return result;
        });

        await _hub.Clients.All.SendAsync("ReceiveMessage", output);

        return Ok(new InsertTestClassResponse { Id = output });
    }


    [HttpGet]
    [Route(Routes.TRIGGER_EMAIL)]
    public async Task<IActionResult> TriggerEmailAsync()
    {
        SendEmailRequest request = new();

        request.Tos.Add(new EmailAddress("mail@ericjansen.com", "Eric Jansen"));
        request.Subject = "Send email trigger from TestController.";
        request.Body.Html = "<h1>Header</h1><p>Body</p>";
        request.Body.PlainText = "TextBody";

        var result = await _emailSenderService.SendAsync(request);

        return Ok(new TriggerEmailResponse { Response = result.ServerResponse });
    }
}