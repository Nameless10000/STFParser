using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateTrafficPoliceApi.Dtos.Auto;
using StateTrafficPoliceApi.Dtos.Driver;
using StateTrafficPoliceApi.Services;

namespace StateTrafficPoliceApi.Controllers
{
    [Route("idx/api2/gibdd/")]
    [ApiController]
    public class StateTrafficPoliceController(ParserService _parserService) : ControllerBase
    {
        [Route("checkGibdd")]
        [HttpPost]
        public async Task<JsonResult> CheckDrivingLicense([FromBody] DrivingLicenseCheckDTO checkDTO)
        {
            return new(await _parserService.CheckDrivingLicense(checkDTO));
        }

        [Route("checkVehicle")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoHistory([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoHistory(checkDTO));
        }

        [Route("checkDtp")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoDtp([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDtp(checkDTO));
        }

        [Route("getEaistoInfo")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoDc([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoDc(checkDTO));
        }

        [Route("checkFines")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoFines([FromBody] AutoCheckGrzDTO checkDTO)
        {
            return new(await _parserService.CheckAutoFines(checkDTO));
        }

        [Route("checkWanted")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoWanted([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoWanted(checkDTO));
        }

        [Route("checkRestricted")]
        [HttpPost]
        public async Task<JsonResult> CheckAutoRestrict([FromBody] AutoCheckVinDTO checkDTO)
        {
            return new(await _parserService.CheckAutoRestrict(checkDTO));
        }
    }
}
