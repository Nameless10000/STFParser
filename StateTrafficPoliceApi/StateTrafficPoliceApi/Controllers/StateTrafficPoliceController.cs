using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateTrafficPoliceApi.Dtos.Auto;
using StateTrafficPoliceApi.Dtos.Driver;
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

        [HttpPost]
        public async Task<JsonResult> CheckAutoHistory([FromBody] AutoCheckDTO checkDTO)
        {
            return new(await _parserService.CheckAutoHistory(checkDTO));
        }
    }
}
