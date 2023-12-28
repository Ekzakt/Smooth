namespace Smooth.Shared.Endpoints;

public static class ConfigurationEndpoints
{
    public const string CONTROLLER = "/api/config";

    public static string MEDIAFILES_OPTIONS() => $"{CONTROLLER}/mediafilesoptions";
    public static string IMAGE_OPTIONS() => $"{CONTROLLER}/imageoptions";
    public static string VIDEO_OPTIONS() => $"{CONTROLLER}/videooptions";
    public static string SOUND_OPTIONS() => $"{CONTROLLER}/soundoptions";
    public static string VERSIONS() => $"{CONTROLLER}/versions";
}
