using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.WebApp.Controllers
{
    [ApiController]
    [Route(WeatherForecastEndpoints.CONTROLLER)]
    public class WeatherForecastsController(
        IWeatherForecastService weatherForecast)
        : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService = weatherForecast;


        [HttpGet(Name = "GetByRowCount")]
        public async Task<IActionResult> GetByRowcount(int? r)
        {
            var result = await _weatherForecastService.GetAllAsync(r);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
