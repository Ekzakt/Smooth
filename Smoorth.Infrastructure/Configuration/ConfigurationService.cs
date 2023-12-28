using Ekzakt.Utilities.Helpers;
using Microsoft.Extensions.Options;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using System;
using System.IO;
using System.Reflection;

namespace Smooth.Api.Infrastructure.Configuration;

public class ConfigurationService(
    IOptions<MediaFilesOptions> mediaFileOptions,
    IOptions<ImageOptions> imageOptions,
    IOptions<VideoOptions> videoOptions,
    IOptions<SoundOptions> soundOptions)
    : IConfigurationService
{
    private readonly MediaFilesOptions _mediaFileOptions = mediaFileOptions.Value;
    private readonly ImageOptions _imageOptions = imageOptions.Value;
    private readonly VideoOptions _videoOptions = videoOptions.Value;
    private readonly SoundOptions _soundOptions = soundOptions.Value;


    public async Task<IMediaFileOptions> GetMediaFileOptions(string mediaFileOptionsName)
    {
        IMediaFileOptions options = mediaFileOptionsName switch
        {
            nameof(ImageOptions) => _imageOptions,
            nameof(VideoOptions) => _videoOptions,
            nameof(SoundOptions) => _soundOptions,
            _ => throw new NotImplementedException()
        };

        IMediaFileOptions result = await Task.Run(() =>
        {
            return options;
        });

        return result;
    }


    public async Task<MediaFilesOptions> GetMediaFilesOptions()
    {
        MediaFilesOptions result = await Task.Run(() =>
        {
            return _mediaFileOptions;
        });

        return result;
    }


    public async Task<AppVersions> GetAppVersionsAsync(Version assemblyVersion, Version environmentVersion)
    {
        AppVersions result = await Task.Run(() =>
        {
            return new AppVersions(assemblyVersion, environmentVersion);
        });

        return result;
    }
}
