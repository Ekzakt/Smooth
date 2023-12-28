using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using System.Reflection;

namespace Smooth.Api.Application.Configuration;

public interface IConfigurationService
{
    Task<MediaFilesOptions> GetMediaFilesOptions();
    Task<IMediaFileOptions> GetMediaFileOptions(string mediaFileOptionsName);
    Task<AppVersions> GetAppVersionsAsync(Version assemblyVersion, Version environmentVersion);
}
