using Microsoft.Extensions.Options;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.MediaFiles.Options;

namespace Smooth.Api.Infrastructure.MediaFiles;

public class AppSettingsConfigurationService(
    IOptions<MediaFilesOptions> mediaFileOptions) : IConfigurationService
{
    private readonly MediaFilesOptions _mediaFileOptions = mediaFileOptions.Value;

    public async Task<MediaFilesOptions> GetMediaFilesConfigurationAsync()
    {
        Task<MediaFilesOptions> task = Task.Run(() =>
        {
            return _mediaFileOptions;
        });

        var result = await task;

        return result;
    }
}
