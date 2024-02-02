using Microsoft.AspNetCore.Mvc;
using Smooth.Api.Application.WeatherForecasts;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers
{
    [ApiController]
    [Route(Ctrls.WEATERFORECASTS)]
    public class WeatherForecastsController(IWeatherForecastService _weatherForecastService, ILogger<WeatherForecastsController> _logger)
        : ControllerBase
    {

        [HttpGet]
        [Route(Routes.GET_WEATERFORECASTS)]
        public async Task<IActionResult> GetByRowcount(int? r, CancellationToken cancellationToken)
        {
            _logger.LogWarning("**** CorrelationId Warning ***");

            var result = await _weatherForecastService.GetAllAsync(r, cancellationToken);

            return result is not null
                ? Ok(result)
                : NoContent();
        }
    }
}
