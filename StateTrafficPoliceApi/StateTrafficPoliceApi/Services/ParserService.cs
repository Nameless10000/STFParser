using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.Dtos.Auto;
using StateTrafficPoliceApi.Dtos.Driver;
using StateTrafficPoliceApi.IdxDtos;
using StateTrafficPoliceApi.IdxDtos.AutoHistory;
using StateTrafficPoliceApi.StfDtos;
using StateTrafficPoliceApi.StfDtos.Auto;
using StateTrafficPoliceApi.StfDtos.Driver;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Services
{
    public partial class ParserService(IMemoryCache cache, IMapper mapper)
    {
        private readonly HttpClient _httpClient = new();

        

        #region Auto

        public async Task<AutoHistoryDTO> CheckAutoHistory(AutoCheckDTO autoCheckDTO)
        {
            var stfDto = await GetResponse<StfAutoResponseDTO, AutoCheckDTO, AutoResolvedDTO>("https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/auto/register", autoCheckDTO, 
                (checkDto, captcha) => AutoResolvedDTO.FromCheck(checkDto, captcha, "history"));

            return mapper.Map<AutoHistoryDTO>(stfDto);
        }

        #endregion


        public async Task<DrivingLicenseDTO> CheckDrivingLicense(DrivingLicenseCheckDTO checkDTO)
        {
            var stdDto = await GetResponse<StfDriverResponseDTO, DrivingLicenseCheckDTO, DrivingLicenseResolvedDTO>("https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/driver", checkDTO, DrivingLicenseResolvedDTO.FromCheck);

            return mapper.Map<DrivingLicenseDTO>(stdDto);
        }

        private async Task SetHeaders()
        {
            var response = await _httpClient.GetAsync("https://гибдд.рф/check/driver");
            var content = await response.Content.ReadAsStringAsync();

            var match = CsrfToken().Match(content);
            var tokenValue = match.Groups[1].Value;


            _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            _httpClient.DefaultRequestHeaders.Add("X-Csrftokensec", tokenValue);
        }

        private async Task<TValue> GetResponse<TValue, TCheckDTO, TResolvedDTO>(string fetchAddress, TCheckDTO checkDTO, Func<TCheckDTO, CaptchaDTO, TResolvedDTO> getResolvedDto)
        {
            var isSuccessStatusCode = false;
            var response = new HttpResponseMessage();

            var i = 0;
            while (!isSuccessStatusCode)
            {
                if (i > 0)
                    await Task.Delay(10000);

                if (!cache.TryGetValue<CaptchaDTO>("captcha", out var resolvedCaphca))
                {
                    await Task.Delay(10000);
                    continue;
                }

                var resolvedDto = getResolvedDto(checkDTO, resolvedCaphca);

                var props = resolvedDto
                    .GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToList();

                var content = new Dictionary<string, string>();

                foreach (var prop in props)
                    content.Add(prop.Name[0].ToString().ToLower() + prop.Name[1..], prop.GetValue(resolvedDto).ToString());

                await SetHeaders();

                response = await _httpClient.PostAsync(fetchAddress, new FormUrlEncodedContent(content));
                i++;
                isSuccessStatusCode = response.StatusCode == HttpStatusCode.OK;
            }

            return await response.Content.ReadFromJsonAsync<TValue>();
        }

        [GeneratedRegex("<meta name=\'csrf-token-value\' content=\'(.+)\'/>")]
        private static partial Regex CsrfToken();
    }
}
