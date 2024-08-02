using Microsoft.AspNetCore.Mvc;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FgisTaxiController(FgisTasxiService fgisTasxiService) : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> FetchTaxi()
        {
            return new(await fgisTasxiService.ParseTaxi());
        }
    }
}
