using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Dtos;
using Ekzakt.Utilities.Helpers;

namespace Smooth.Api.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> InsertTestClass(InsertTestClassRequestDto request)
    {
        var output = await Task.Run(() =>
        {
            var result = IntHelpers.GetRandomPositiveInt();

            return result;
        });

        return Ok(new InsertTestClassResponsDto { Id = output });
    }
}


