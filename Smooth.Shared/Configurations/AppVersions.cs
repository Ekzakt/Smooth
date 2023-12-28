using Ekzakt.Utilities.Helpers;

namespace Smooth.Shared.Configurations;

public class AppVersions
{
    public string Build { get; set; } = string.Empty;
    public string FrameWork { get; init; } = string.Empty;


    public AppVersions() { }


    public AppVersions(Version assemblyVersion, Version environmentVersion)
    {
        Build = assemblyVersion?.Format()!;
        FrameWork = Environment.Version.ToString();
    }
}
