using Microsoft.AspNetCore.Mvc;
using Smooth.Application.WeatherForecasts;

namespace Smooth.Api.WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastsController(
        ILogger<WeatherForecastsController> logger,
        IWeatherForecastService weatherForecast)
        : ControllerBase
    {
        private readonly ILogger<WeatherForecastsController> _logger = logger;
        private readonly IWeatherForecastService _weatherForecastService = weatherForecast;


        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get(int? r)
        {
            var result = await _weatherForecastService.GetAllAsync(r);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
