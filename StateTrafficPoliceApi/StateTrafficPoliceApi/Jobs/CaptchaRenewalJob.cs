using Microsoft.Extensions.Caching.Memory;
using Quartz;
using StateTrafficPoliceApi.Dtos;
using StateTrafficPoliceApi.Services;
using StateTrafficPoliceApi.StfDtos;
using System.Net.Http;

namespace StateTrafficPoliceApi.Jobs
{
    public class CaptchaRenewalJob(IMemoryCache cache, FlaskService flaskService) : IJob
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
            var captcha = (await response.Content.ReadFromJsonAsync<StfCaptchaDTO>())!;

            var captchaWord = await flaskService.SolveGibddCaptchaAsync(captcha.Bytes);

            var solvedCapcha = CaptchaDTO.FromStf(captcha, captchaWord);

            cache.Set("captcha", solvedCapcha, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }
    }
}
