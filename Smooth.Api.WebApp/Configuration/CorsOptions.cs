using static System.Net.WebRequestMethods;

namespace Smooth.Api.WebApp.Configuration;

public class CorsOptions
{
    public const string OptionsName = "CorsValues";

    public static readonly string[] CorsValues = { "https://localhost:7083" };
}
