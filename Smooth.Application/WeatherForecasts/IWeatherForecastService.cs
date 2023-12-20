namespace Smooth.Application.WeatherForecasts;

public interface IWeatherForecastService
{
    Task<List<WeatherForecast>?> GetAllAsync(int? number);
}