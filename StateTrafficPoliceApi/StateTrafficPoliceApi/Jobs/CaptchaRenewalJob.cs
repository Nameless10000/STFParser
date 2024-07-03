using Microsoft.Extensions.Caching.Memory;
using Quartz;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.StfDtos;
using System.Net.Http;

namespace StateTrafficPoliceApi.Jobs
{
    public class CaptchaRenewalJob(IMemoryCache cache) : IJob
    {
        private readonly HttpClient _httpClient = new();

        public async Task Execute(IJobExecutionContext context)
        {
            cache.Remove("captcha");

            await SolveCapcha();
        }

        private async Task SolveCapcha()
        {
            var response = await _httpClient.GetAsync("https://check.gibdd.ru/captcha");
            var capcha = (await response.Content.ReadFromJsonAsync<StfCaptchaDTO>())!;

            using var fs = File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Gibdd Capcha.jpeg"));
            fs.Write(capcha.Bytes);
            fs.Close();

            Console.WriteLine("Введите решение капчи с рабочего стола (5 цифр)");
            var capchaWord = Console.ReadLine();

            var solvedCapcha = CaptchaDTO.FromStf(capcha, capchaWord);

            cache.Set("captcha", solvedCapcha, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
    }
}
