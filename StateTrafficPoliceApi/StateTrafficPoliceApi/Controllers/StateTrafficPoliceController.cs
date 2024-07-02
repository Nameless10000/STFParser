using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateTrafficPoliceController(ParserService _parserService) : ControllerBase
    {
        [HttpPost]
        public async Task<JsonResult> CheckDrivingLicense([FromBody] DrivingLicenseCheckDTO checkDTO)
        {
            return new(await _parserService.CheckDrivingLicense(checkDTO));
        }
    }
}
