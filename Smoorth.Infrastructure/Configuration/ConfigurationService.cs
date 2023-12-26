using Ekzakt.Core.Helpers;
using Microsoft.Extensions.Options;
using Smooth.Api.Application.Configuration;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using System.Reflection;

namespace Smooth.Api.Infrastructure.Configuration;

public class ConfigurationService(
    IOptions<MediaFilesOptions> mediaFileOptions)
    : IConfigurationService
{
    private readonly MediaFilesOptions _mediaFileOptions = mediaFileOptions.Value;

    public async Task<MediaFilesOptions> GetMediaFilesConfigurationAsync()
    {
        MediaFilesOptions result = await Task.Run(() =>
        {
            return _mediaFileOptions;
        });

        return result;
    }


    public async Task<Versions> GetVersionsAsync(Assembly assembly)
    {
        var result = await Task.Run(() =>
        {
            var buildVersion = assembly.GetVersionFormattedString();

            return new Versions(Environment.Version.ToString())
            {
                Build = buildVersion ?? string.Empty
            };
        });

        return result;
    }
}
