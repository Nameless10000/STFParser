using Microsoft.Extensions.Options;
using StateTrafficPoliceApi.Configured;
using StateTrafficPoliceApi.Dtos;
using System.Net.Http.Headers;

namespace StateTrafficPoliceApi.Services
{
    public class FlaskService(IOptions<FlaskData> _flaskData)
    {
        private readonly HttpClient _httpClient = new();

        public async Task<string?> SolveGibddCaptchaAsync(byte[] captchaBytes)
        {
            using var captchaStream = new MemoryStream(captchaBytes);

            captchaStream.Position = 0;

            var streamContent = new StreamContent(captchaStream);

            var multipartFormDataContent = new MultipartFormDataContent
            {
                { streamContent, "file", "captcha.jpeg" }
            };

            var requestPath = $"{_flaskData.Value.DefaultPath}/gibdd-captcha";

            FlaskResponseDTO? content = null;
            string errorContent = "";

            var response = await _httpClient.PostAsync(requestPath, multipartFormDataContent);

            if (response.IsSuccessStatusCode)
                content = await response.Content.ReadFromJsonAsync<FlaskResponseDTO>();
            else
                errorContent = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(errorContent);

            return content?.Prediction;
        }
    }
}
