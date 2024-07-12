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
        public async Task<JsonResult> CheckAutoHistory([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoHistory(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckAutoDtp([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDtp(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckAutoDc([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDc(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckAutoFines([FromBody] AutoCheckGrzDTO checkDTO)
        {
            return new(await _parserService.CheckAutoFines(checkDTO));
        }

        [HttpPost]
        public async Task<JsonResult> CheckAutoWanted([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoWanted(checkDTO));
        }
    }
}
