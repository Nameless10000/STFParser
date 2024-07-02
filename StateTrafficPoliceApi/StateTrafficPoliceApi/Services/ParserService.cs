using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.StfDtos;

namespace StateTrafficPoliceApi.Services
{
    public class ParserService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<CapchaDTO> GetCapcha()
        {
            var response = await _httpClient.GetAsync("https://check.gibdd.ru/captcha");
            var capcha = (await response.Content.ReadFromJsonAsync<StfCapchaDTO>())!;

            using var fs = File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Gibdd Capcha.jpeg"));
            fs.Write(capcha.Bytes);
            fs.Close();

            Console.WriteLine("Введите решение капчи с рабочего стола (5 цифр)");
            var capchaWord = Console.ReadLine();

            return CapchaDTO.FromStf(capcha, capchaWord);
        }

        public async Task<StfResponseDTO> CheckDrivingLicense(DrivingLicenseCheckDTO checkDTO)
        {
            var resolvedCaphca = await GetCapcha();

            var resolvedDto = DrivingLicenseResolvedDTO.FromCheck(checkDTO, resolvedCaphca);

            var content = new MultipartFormDataContent()
            {
                { new StringContent(resolvedDto.Date), "date"},
                { new StringContent(resolvedDto.Num), "num"},
                { new StringContent(resolvedDto.CapchaWord), "capchaWord"},
                { new StringContent(resolvedDto.CapchaToken), "capchaToken"},
            };
            var response = await _httpClient.PostAsync("https://xn--b1afk4ade.xn--90adear.xn--p1ai/proxy/check/driver", content);
            var stfResponse = await response.Content.ReadFromJsonAsync<StfResponseDTO>();

            return stfResponse;
        } 
    }
}
