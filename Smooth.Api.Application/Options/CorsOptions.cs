namespace Smooth.Api.Application.Options;

public class CorsOptions
{
    public const string OptionsName = "Cors";

    public const string POLICY_NAME = "AllowedOrigins";

    //public static readonly string[] AllowedOrigins = { "https://localhost:7083", "https://dev.ekzakt.be" };
    public string[] AllowedOrigins { get; init; } = [];
}
