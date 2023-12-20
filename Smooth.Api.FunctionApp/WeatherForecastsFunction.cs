using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Smooth.Application.Azure;
using Smooth.Application.WeatherForecasts;
using System.Net;

namespace Smooth.Api.FunctionApp.FunctionApp;

public class WeatherForecastsFunction(
        ILogger<WeatherForecastsFunction> logger,
        IOptions<AzureOptions> options,
        IWeatherForecastService weatherForecastService
    )
{
    private readonly AzureOptions _options = options.Value;
    private readonly ILogger<WeatherForecastsFunction> _logger = logger;
    private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;


    [Function("WeatherForecasts")]
    public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        Int32.TryParse(req.Query["r"], out int r);
        r = r <= 0 ? 10 : r;

        var result = await _weatherForecastService.GetAllAsync(r);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(result);

        return response;
    }
}
