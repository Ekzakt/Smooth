using Smooth.Api.Application.Options;
using Smooth.Shared.Configurations;
using Smooth.Shared.Configurations.MediaFiles.Options;
using System.Reflection;

namespace Smooth.Api.Application.Configuration;

public interface IConfigurationService
{
    Task<MediaFilesOptions> GetMediaFilesOptionsAsync();
    Task<IMediaFileOptions> GetMediaFileOptionsAsync(string mediaFileOptionsName);
    Task<AppVersions> GetAppVersionsAsync(Version assemblyVersion, Version environmentVersion);
    Task<AzureOptions> GetAzureOptionsAsync();
}
