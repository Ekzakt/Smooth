namespace Smooth.Client.Application.Endpoints;

public static class WeatherForecastEndpoints
{
    public static string GET_BY_ROWCOUNT(int? rowCount) => $"/api/weatherforecasts?r={rowCount ?? 10}";
}
