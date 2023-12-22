namespace Smooth.Shared.Endpoints;

public static class ConfigurationEndpoints
{
    public const string CONTROLLER = "/api/config";

    public static string MEDIAFILES() => $"{CONTROLLER}/mediafiles";
    public static string VERSIONS() => $"{CONTROLLER}/versions";
}
