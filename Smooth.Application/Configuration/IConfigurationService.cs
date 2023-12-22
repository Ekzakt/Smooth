using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using System.Reflection;

namespace Smooth.Api.Application.Configuration;

public interface IConfigurationService
{
    Task<MediaFilesOptions> GetMediaFilesConfigurationAsync();

    Task<Versions> GetVersionsAsync(Assembly assembly);
}