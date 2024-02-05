using Microsoft.Extensions.Options;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.Options.Azure;
using Smooth.Shared.Configurations.Options.MediaFiles;

namespace Smooth.Api.Infrastructure.Configuration;

public class ConfigurationService
    : IConfigurationService
{
    private readonly MediaFilesOptions _mediaFileOptions;
    private readonly AzureOptions _azureOptions;


    public ConfigurationService(IOptions<MediaFilesOptions> mediaFileOptions, IOptions<AzureOptions> azureOptions)
    {
        _mediaFileOptions = mediaFileOptions.Value;
        _azureOptions = azureOptions.Value;
    }


    public async Task<IMediaFileOptions> GetMediaFileOptionsAsync(string mediaFileOptionsName)
    {
        IMediaFileOptions options = mediaFileOptionsName switch
        {
            nameof(ImageOptions) => _mediaFileOptions.Images,
            nameof(VideoOptions) => _mediaFileOptions.Videos,
            nameof(SoundOptions) => _mediaFileOptions.Sounds,
            _ => throw new NotImplementedException()
        };

        IMediaFileOptions result = await Task.Run(() =>
        {
            return options;
        });

        return result;
    }


    public async Task<MediaFilesOptions> GetMediaFilesOptionsAsync()
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


    public async Task<AzureOptions> GetAzureOptionsAsync()
    {
        AzureOptions result = await Task.Run(() =>
        {
            return _azureOptions;
        });

        return result;
    }
}
