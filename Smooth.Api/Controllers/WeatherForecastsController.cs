using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers
{
    [ApiController]
    [Route(Ctrls.WEATERFORECASTS)]
    public class WeatherForecastsController
        : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;


        public WeatherForecastsController(IWeatherForecastService weatherForecast)
        {
            _weatherForecastService = weatherForecast;
        }


        [HttpGet]
        [Route(Routes.GET_WEATERFORECASTS)]
        public async Task<IActionResult> GetByRowcount(int? r, CancellationToken cancellationToken)
        {
            var result = await _weatherForecastService.GetAllAsync(r, cancellationToken);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
