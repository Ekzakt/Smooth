using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.Configurations.MediaFiles.Options;

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
        [Route("MediaFilesOptions")]
        public async Task<IActionResult> GetMediaFilesOptionsAsync()
        {
            var result = await _configurationService.GetMediaFilesOptionsAsync();

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("ImageOptions")]
        public async Task<IActionResult> GetImageOptionsAsync()
        {
            //var result = await _configurationService.GetImageOptionsAsync();

            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(ImageOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("VideoOptions")]
        public async Task<IActionResult> GetVideoOptionsAsync()
        {
            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(VideoOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("SoundOptions")]
        public async Task<IActionResult> GetSoundOptionsAsync()
        {
            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(SoundOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


#if DEBUG
        [HttpGet]
        [Route("AzureOptions")]
        public async Task<IActionResult> GetAzureOptionsAsync()
        {
            var result = await _configurationService.GetAzureOptionsAsync();

            return result is not null
                ? Ok(result)
                : NoContent();
        }
#endif

        [HttpGet]
        [Route("AppVersions")]
        public async Task<IActionResult> GetBuildVersionAsync()
        {
            var result = await _configurationService.GetAppVersionsAsync(typeof(Program).Assembly.GetName()?.Version!, Environment.Version);

            return result is not null
                ? Ok(result)
                : NoContent();
        }

    }
}
