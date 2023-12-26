using System.Text.Json.Serialization;

namespace Smooth.Shared.Configurations;

public class Versions
{
    public string Build { get; set; } = string.Empty;
    public string FrameWork { get; init; } = string.Empty;


    public Versions() { }


    public Versions(string environmentVersion)
    {
        FrameWork = environmentVersion;
    }
}
