using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Quartz;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.Dtos.Auto;
using StateTrafficPoliceApi.Dtos.Driver;
using StateTrafficPoliceApi.IdxDtos.Auto.DiagnosticCard;
using StateTrafficPoliceApi.IdxDtos.Auto.DTP;
using StateTrafficPoliceApi.IdxDtos.Auto.Fines;
using StateTrafficPoliceApi.IdxDtos.Auto.Hostory;
using StateTrafficPoliceApi.IdxDtos.Auto.Restrict;
using StateTrafficPoliceApi.IdxDtos.Auto.Wanted;
using StateTrafficPoliceApi.IdxDtos.Driver;
using StateTrafficPoliceApi.StfDtos;
using StateTrafficPoliceApi.StfDtos.Auto.DiagnosticCard;
using StateTrafficPoliceApi.StfDtos.Auto.DTP;
using StateTrafficPoliceApi.StfDtos.Auto.Fines;
using StateTrafficPoliceApi.StfDtos.Auto.History;
using StateTrafficPoliceApi.StfDtos.Auto.Restrict;
using StateTrafficPoliceApi.StfDtos.Auto.Wanted;
using StateTrafficPoliceApi.StfDtos.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Services
{
    public partial class ParserService(IMemoryCache cache, IMapper mapper, ISchedulerFactory schedulerFactory)
    {
        private readonly HttpClient _httpClient = new();

        #region Auto

        public async Task<IdxAutoRestrictListDTO> CheckAutoRestrict(AutoCheckVinDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoRestrictResponseDTO, AutoCheckVinDTO, AutoResolvedVinDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/restrict",
                autoCheckDTO,
                (checkDto, captcha) => AutoResolvedVinDTO.FromCheck(checkDto, captcha, "restricted")
                );

            return mapper.Map<IdxAutoRestrictListDTO>(stfDto);
        }

        public async Task<IdxAutoWantedListDTO> CheckAutoWanted(AutoCheckVinDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoWantedResponseDTO, AutoCheckVinDTO, AutoResolvedVinDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/wanted",
                autoCheckDTO,
                (checkDto, captcha) => AutoResolvedVinDTO.FromCheck(checkDto, captcha, "wanted")
                );

            return mapper.Map<IdxAutoWantedListDTO>(stfDto);
        }

        public async Task<IdxAutoFinesListDTO> CheckAutoFines(AutoCheckGrzDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoFinesResponseDTO, AutoCheckGrzDTO, AutoResolvedGrzDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/fines",
                autoCheckDTO,
                AutoResolvedGrzDTO.FromCheck);

            stfDto.Data.ForEach(x => x.Divisions = stfDto.Divisions);

            return mapper.Map<IdxAutoFinesListDTO>(stfDto);
        }

        public async Task<IdxAutoHistoryDTO> CheckAutoHistory(AutoCheckVinDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoHistoryResponseDTO, AutoCheckVinDTO, AutoResolvedVinDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/register", 
                autoCheckDTO, 
                (checkDto, captcha) => AutoResolvedVinDTO.FromCheck(checkDto, captcha, "history")
                );

            return mapper.Map<IdxAutoHistoryDTO>(stfDto);
        }
        
        public async Task<IdxAutoDtpDTO> CheckAutoDtp(AutoCheckVinDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoDTPResponseDTO, AutoCheckVinDTO, AutoResolvedVinDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/dtp", 
                autoCheckDTO, 
                (checkDto, captcha) => AutoResolvedVinDTO.FromCheck(checkDto, captcha, "aiusdtp")
                );

            return mapper.Map<IdxAutoDtpDTO>(stfDto);
        }
        
        public async Task<IdxAutoDcListDTO> CheckAutoDc(AutoCheckVinDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoDCResponseDTO, AutoCheckVinDTO, AutoResolvedVinDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/diagnostic", 
                autoCheckDTO, 
                (checkDto, captcha) => AutoResolvedVinDTO.FromCheck(checkDto, captcha, "diagnostic")
                );

            var convertedStfDto = new ConvertedAutoDCResponseDTO();

            stfDto.RequestResult.DiagnosticCards[0].PreviousDcs.ForEach(x => x.Vin = stfDto.RequestResult.DiagnosticCards[0].Vin);

            convertedStfDto.List = 
                [
                    new StfAutoShortDcDTO() 
                    {
                        Vin = stfDto.RequestResult.DiagnosticCards[0].Vin,
                        DcDate = stfDto.RequestResult.DiagnosticCards[0].DcDate,
                        DcExpirationDate = stfDto.RequestResult.DiagnosticCards[0].DcExpirationDate,
                        DcNumber = stfDto.RequestResult.DiagnosticCards[0].DcNumber,
                        OdometerValue = stfDto.RequestResult.DiagnosticCards[0].OdometerValue
                    }, 
                    .. stfDto.RequestResult.DiagnosticCards[0].PreviousDcs 
                ];

            return mapper.Map<IdxAutoDcListDTO>(convertedStfDto);
        }

        #endregion


        public async Task<IdxDrivingLicenseDTO> CheckDrivingLicense(DrivingLicenseCheckDTO checkDTO)
        {
            var stdDto = await GetResponse<StfDriverResponseDTO, DrivingLicenseCheckDTO, DrivingLicenseResolvedDTO>(
                "https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/driver", 
                checkDTO, 
                DrivingLicenseResolvedDTO.FromCheck
                );

            return mapper.Map<IdxDrivingLicenseDTO>(stdDto);
        }

        #region Helpers

        private async Task SetHeaders()
        {
            var response = await _httpClient.GetAsync("https://гибдд.рф/check/driver");
            var content = await response.Content.ReadAsStringAsync();

            var match = CsrfToken().Match(content);
            var tokenValue = match.Groups[1].Value;


            _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            _httpClient.DefaultRequestHeaders.Add("X-Csrftokensec", tokenValue);
        }

        private async Task<TValue> GetResponse<TValue, TCheckDTO, TResolvedDTO>(string fetchAddress, TCheckDTO checkDTO, Func<TCheckDTO, CaptchaDTO, TResolvedDTO> getResolvedDto) where TValue : AbstractResponseDTO
        {
            var responseResult = default(TValue);
            while (responseResult == null)
            {

                if (!cache.TryGetValue<CaptchaDTO>("captcha", out var resolvedCaphca))
                    continue;

                var resolvedDto = getResolvedDto(checkDTO, resolvedCaphca);

                var props = resolvedDto
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToList();

                var content = new Dictionary<string, string>();

                foreach (var prop in props)
                {
                    var value = prop.GetValue(resolvedDto);
                    if (value == null)
                        continue;

                    content.Add(prop.Name[0].ToString().ToLower() + prop.Name[1..], value.ToString());
                }

                await SetHeaders();

                var response = await _httpClient.PostAsync(fetchAddress, new FormUrlEncodedContent(content));

                responseResult = await response.Content.ReadFromJsonAsync<TValue>();

                if (responseResult.Message == "Проверка CAPTCHA не была пройдена из-за неверного введенного значения.")
                {
                    var scheduler = await schedulerFactory.GetScheduler();
                    await scheduler.TriggerJob(JobKey.Create("CaptchaRenewalJob", "group1"));
                    continue;
                }
            }

            return responseResult;
        }

        [GeneratedRegex("<meta name=\'csrf-token-value\' content=\'(.+)\'/>")]
        private static partial Regex CsrfToken();

        #endregion
    }
}
