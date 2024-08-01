using Microsoft.AspNetCore.Html;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Services
{
    public partial class FgisTasxiService(StfDbContext stfDbContext)
    {
        private class Captcha
        {
            public string Cimg { get; set; }

            public int Cid { get; set; }

            public byte[] Bytes => Convert.FromBase64String(Cimg.Split("base64,")[1].Replace(" ", ""));

            public string Solution { get; set; }
        }

        private readonly string _host = "https://sicmt.ru/fgis-taksi";
        private readonly HttpClient _httpClient = new();

        public async Task ParseTaxi()
        {
            var captchaData = await SolveCaptcha();

            var carIDs = new List<string>();
            var htmlContents = new List<string>();
            foreach (var page in Enumerable.Range(1, 4))
            {
                var onePagePath = $"{_host}/page/{page}?type=car&filters%5BregNumTS%5D&filters%5Bvin%5D=Z&captcha={captchaData.Solution}&cid={captchaData.Cid}";

                var response = await _httpClient.GetAsync(onePagePath);

                htmlContents.Add(await response.Content.ReadAsStringAsync());
            }


            foreach (var htmlContent in htmlContents)
            {
                var matches = RowsRegex().Matches(htmlContent);


                foreach (var match in matches.Cast<Match>())
                {
                    carIDs.Add(match.Groups[1].Value);
                }
            }
            
        }

        private async Task<Captcha> SolveCaptcha()
        {
            var path = "https://sicmt.ru/wp-content/ajax.php";

            var content = new MultipartFormDataContent
            {
                { new StringContent("check_captcha"), "action" },
                { new StringContent("\"1122043340"), "id" },
                { new StringContent("2Д7Ю4"), "val" },
                { new StringContent("0"), "let" },
            };

            var response = await _httpClient.PostAsync(path, content);

            var captcha = await response.Content.ReadFromJsonAsync<Captcha>();

            using var fs = File.Create("C:\\Users\\Евгений\\Desktop\\captcha.png");
            await fs.WriteAsync(captcha.Bytes);
            fs.Close();
            await Console.Out.WriteLineAsync("Введите решение капчи:");
            captcha.Solution = Console.ReadLine();

            return captcha;
        }

        [GeneratedRegex(@"tr id=""(.+)""")]
        private static partial Regex RowsRegex();
    }
}
