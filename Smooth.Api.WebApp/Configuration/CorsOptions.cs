namespace Smooth.Api.WebApp.Configuration;

public class CorsOptions
{
    public const string POLICY_NAME = "_MyAllowedOrigins";

    public static readonly string[] CorsValues = { "https://localhost:7083", "https://wonderful-desert-02c68e403.4.azurestaticapps.net", "https://dev.ekzakt.be" };
}
