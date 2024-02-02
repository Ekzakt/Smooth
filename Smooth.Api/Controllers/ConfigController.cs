﻿using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.Configurations.MediaFiles.Options;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers
{
    [Route(Ctrls.CONFIGURATION)]
    [ApiController]
    public class ConfigController
        : ControllerBase
    {
        private readonly IConfigurationService _configurationService;


        public ConfigController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }



        [HttpGet]
        [Route(Routes.GET_MEDIAFILES_OPTIONS)]
        public async Task<IActionResult> GetMediaFilesOptionsAsync()
        {
            var result = await _configurationService.GetMediaFilesOptionsAsync();

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route(Routes.GET_IMAGE_OPTIONS)]
        public async Task<IActionResult> GetImageOptionsAsync()
        {
            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(ImageOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route(Routes.GET_VIDEO_OPTIONS)]
        public async Task<IActionResult> GetVideoOptionsAsync()
        {
            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(VideoOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


        [HttpGet]
        [Route(Routes.GET_SOUND_OPTIONS)]
        public async Task<IActionResult> GetSoundOptionsAsync()
        {
            var result = await _configurationService.GetMediaFileOptionsAsync(nameof(SoundOptions));

            return result is not null
                ? Ok(result)
                : NoContent();
        }


#if DEBUG
        [HttpGet]
        [Route(Routes.GET_AZURE_OPTIONS)]
        public async Task<IActionResult> GetAzureOptionsAsync()
        {
            var result = await _configurationService.GetAzureOptionsAsync();

            return result is not null
                ? Ok(result)
                : NoContent();
        }
#endif

        [HttpGet]
        [Route(Routes.GET_APP_VERSIONS)]
        public async Task<IActionResult> GetBuildVersionAsync()
        {
            var result = await _configurationService.GetAppVersionsAsync(typeof(Program).Assembly.GetName()?.Version!, Environment.Version);

            return result is not null
                ? Ok(result)
                : NoContent();
        }

    }
}
