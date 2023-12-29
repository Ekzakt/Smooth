namespace Smooth.Api.WebApp.Configuration;

public class CorsOptions
{
    public const string POLICY_NAME = "_MyAllowedOrigins";

    public static readonly string[] CorsValues = ["https://localhost:7083", "https://dev.ekzakt.be"];
}
