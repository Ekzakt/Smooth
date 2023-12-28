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
            var result = await _configurationService.GetMediaFilesOptions();

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("ImageOptions")]
        public async Task<IActionResult> GetImageOptions()
        {
            //var result = await _configurationService.GetImageOptionsAsync();

            var result = await _configurationService.GetMediaFileOptions(nameof(ImageOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("VideoOptions")]
        public async Task<IActionResult> GetVideoOptions()
        {
            //var result = await _configurationService.GetVideoOptionsAsync();

            var result = await _configurationService.GetMediaFileOptions(nameof(VideoOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("SoundOptions")]
        public async Task<IActionResult> GetSoundOptions()
        {
            //var result = await _configurationService.GetSoundOptionsAsync();

            var result = await _configurationService.GetMediaFileOptions(nameof(SoundOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route("Versions")]
        public async Task<IActionResult> GetBuildVersion()
        {
            var result = await _configurationService.GetAppVersionsAsync(typeof(Program).Assembly.GetName()?.Version!, Environment.Version);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
