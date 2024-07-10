using Microsoft.Extensions.Caching.Memory;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.StfDtos;
using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Services
{
    public partial class ParserService(IMemoryCache cache)
    {
        private readonly HttpClient _httpClient = new();

        private async Task SetHeaders()
        {
            var response = await _httpClient.GetAsync("https://гибдд.рф/check/driver");
            var content = await response.Content.ReadAsStringAsync();

            var match = CsrfToken().Match(content);
            var tokenValue = match.Groups[1].Value;


            _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            _httpClient.DefaultRequestHeaders.Add("X-Csrftokensec", tokenValue);
        }

        

        public async Task<StfResponseDTO> CheckDrivingLicense(DrivingLicenseCheckDTO checkDTO)
        {
            var isSuccessStatusCode = false;
            var response = new HttpResponseMessage();

            var i = 0;
            while (!isSuccessStatusCode)
            {
                if (i > 0)
                    await Task.Delay(10000);

                if (!cache.TryGetValue<CaptchaDTO>("captcha", out var resolvedCaphca))
                    continue;

                var resolvedDto = DrivingLicenseResolvedDTO.FromCheck(checkDTO, resolvedCaphca);

                var content = new Dictionary<string, string>()
                {
                    { "date", resolvedDto.Date },
                    { "num", resolvedDto.Num },
                    { "captchaWord", resolvedDto.CapchaWord },
                    { "captchaToken", resolvedDto.CapchaToken },
                };

                await SetHeaders();
            
                response = await _httpClient.PostAsync("https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/driver", new FormUrlEncodedContent(content));
                i++;
                isSuccessStatusCode = response.IsSuccessStatusCode;
            }

            return await response.Content.ReadFromJsonAsync<StfResponseDTO>();
        }

        [GeneratedRegex("<meta name=\'csrf-token-value\' content=\'(.+)\'/>")]
        private static partial Regex CsrfToken();
    }
}
