namespace Smooth.Shared.Endpoints;

public static class WeatherForecastEndpoints
{
    public const string CONTROLLER = "/api/weatherforecasts";
    public static string GET_BY_ROWCOUNT(int? rowCount) => $"{CONTROLLER}?r={rowCount ?? 10}";
}
