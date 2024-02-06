using Ekzakt.EmailSender.Core.Contracts;
using Ekzakt.EmailSender.Core.Models;
using Ekzakt.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Smooth.Api.SignalR;
using Smooth.Shared.Endpoints;
using Smooth.Shared.Models.Requests;
using Smooth.Shared.Models.Responses;

namespace Smooth.Api.Controllers;


[Route(Ctrls.CORS)]
[ApiController]
public class CorsController(
    IHubContext<NotificationsHub> _hub,
    IEmailSenderService _emailSenderService
    ) : ControllerBase
{
    [HttpGet]
    [Route(Routes.GET_RANDOM_GUID)]
    public IActionResult GetRandomValue()
    {
        var output = Guid.NewGuid();

        return Ok(output);
    }


    [HttpPost]
    [Route(Routes.POST_GUID)] 
    public IActionResult PostGuidValue(Guid value)
    {
        return Ok( new { Value = value });
    }


    //[HttpPost]
    //[Route(Routes.INSERT_TESTCLASS)]
    //public async Task<IActionResult> InsertTestClassAsync(InsertTestClassRequest request)
    //{
    //    var output = await Task.Run(() =>
    //    {
    //        var result = IntHelpers.GetRandomPositiveInt();

    //        return result;
    //    });

    //    //await _hub.Clients.All.SendAsync("ReceiveMessage", output);

    //    return Ok(new InsertTestClassResponse { Id = output });
    //}



    //[HttpGet]
    //[Route(Routes.TRIGGER_EMAIL)]
    //public async Task<IActionResult> TriggerEmailAsync()
    //{
    //    SendEmailRequest request = new();

    //    request.Tos.Add(new EmailAddress("mail@ericjansen.com", "Eric Jansen"));
    //    request.Subject = "Send email trigger from TestController.";
    //    request.Body.Html = "<h1>Header</h1><p>Body</p>";
    //    request.Body.PlainText = "TextBody";

    //    var result = await _emailSenderService.SendAsync(request);

    //    return Ok(new TriggerEmailResponse { Response = result.ServerResponse });
    //}
}
