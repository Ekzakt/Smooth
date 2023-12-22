using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.Configuration;

namespace Smooth.Api.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController(
        IConfigurationService configurationService)
        : ControllerBase
    {
        private readonly IConfigurationService _configurationService = configurationService;

        [HttpGet]
        [Route("MediaFiles")]
        public async Task<IActionResult> GetMediaFilesConfiurationAsync()
        {
            var result = await _configurationService.GetMediaFilesConfigurationAsync();

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("Versions")]
        public async Task<IActionResult> GetFrameWorkVersionAsync()
        {
            var result = await _configurationService.GetVersionsAsync(typeof(Program).Assembly);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
