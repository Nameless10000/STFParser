using Microsoft.AspNetCore.Mvc;
using StateTrafficPoliceApi.Dtos.Auto;
using StateTrafficPoliceApi.Dtos.Driver;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GibddController(ParserService _parserService) : ControllerBase
    {
        [HttpPost]
        public async Task<JsonResult> CheckGibdd([FromBody] DrivingLicenseCheckDTO checkDTO)
        {
            return new(await _parserService.CheckDrivingLicense(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckVehicle([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoHistory(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckDtp([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDtp(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> GetEaistoInfo([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDc(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckFines([FromBody] AutoCheckGrzDTO checkDTO)
        {
            return new(await _parserService.CheckAutoFines(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckWanted([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoWanted(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckRestricted([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoRestrict(checkDTO));
        }
    }
}
