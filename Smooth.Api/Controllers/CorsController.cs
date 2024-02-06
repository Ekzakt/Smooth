using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smooth.Shared.Endpoints;

namespace Smooth.Api.Controllers
{

    [Route(Ctrls.CORS)]
    [ApiController]
    public class CorsController : ControllerBase
    {
        [HttpGet]
        [Route(Routes.GET_RANDOM_GUID)]
        public IActionResult GetRandomValue()
        {
            var output = Guid.NewGuid();

            return Ok(output);
        }


        [HttpPost]
        [Route(Routes.POST_GUID)] 
        public IActionResult PostGuidValue(Guid value)
        {
            return Ok( new { Value = value });
        }
    }
}
