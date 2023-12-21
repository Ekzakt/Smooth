using Microsoft.Extensions.Logging;
using Smooth.Api.Application.WeatherForecasts;

namespace Smoorth.Infrastructure.WeatherForecasts;

public class WeatherForecastService(
    ILogger<WeatherForecastService> logger
    ) 
    : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger = logger;

    public async Task<List<WeatherForecast>?> GetAllAsync(int? rowCount)
    {
        var randomNumber = new Random();
        var temp = 0;
        
        var result = Enumerable.Range(1, CheckRowcount(rowCount)).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = temp = randomNumber.Next(-20, 55),
            Summary = GetSummary(temp)
        }).ToList();

        await Task.Delay(1);

        return result;
    }

    
    #region Helpers

    private string GetSummary(int temp)
    {
        var summary = "Mild";
        if (temp >= 32)
        {
            summary = "Hot";
        }
        else if (temp <= 16 && temp > 0)
        {
            summary = "Cold";
        }
        else if (temp <= 0)
        {
            summary = "Freezing";
        }
        return summary;
    }

    private int CheckRowcount(int? rowCount)
    {
        var output = rowCount.GetValueOrDefault(10);

        output = output <= 1 ? 10 : output;

        return output;
    }

    #endregion Helpers
}
