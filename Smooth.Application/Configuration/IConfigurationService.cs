using Smooth.Shared.MediaFiles.Options;

namespace Smooth.Api.Application.Configuration;

public interface IConfigurationService
{
    Task<MediaFilesOptions> GetMediaFilesConfigurationAsync();
}