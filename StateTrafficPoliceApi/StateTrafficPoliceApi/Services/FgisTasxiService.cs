using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StateTrafficPoliceApi.DbEntities;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Services
{
    public partial class FgisTasxiService(StfDbContext stfDbContext, IWebHostEnvironment env)
    {
        private class Captcha
        {
            public string Cimg { get; set; }

            public int Cid { get; set; }

            public byte[] Bytes => Convert.FromBase64String(Cimg.Split("base64,")[1].Replace(" ", ""));

            public string Solution { get; set; }
        }

        private readonly string _host = "https://sicmt.ru/fgis-taksi";

        private CookieContainer cookieContainer = new();
       
        private HttpClient _httpClient;

        public async Task<int> ParseTaxi()
        {
            _httpClient = new(new HttpClientHandler() { CookieContainer = cookieContainer });

            var lastPage = 0;
            var firstPage = 336;

            var captchaData = await SolveCaptcha();

            var carIDs = new List<string>();
            var htmlDocs = new List<HtmlDocument>();
            foreach (var page in Enumerable.Range(firstPage, 94959))
            {
                var onePagePath = $"{_host}/page/{page}?type=car&filters%5BregNumTS%5D&filters%5Bvin%5D=Z&captcha={captchaData.Solution}&cid={captchaData.Cid}";


                var response = await _httpClient.GetAsync(onePagePath);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync());

                if (page == firstPage)
                {
                    var div = htmlDoc.DocumentNode.SelectSingleNode("(//div[@data-captcha-checked-cookie])");
                    var cookieVal = div.GetAttributeValue("data-captcha-checked-cookie", "");
                    if (cookieVal == "")
                        break;

                    cookieContainer.Add(new Cookie("captchaIsValid", cookieVal, "/", "sicmt.ru"));
                }

                try
                {
                    if (htmlDoc.DocumentNode.SelectNodes("//tr").Count == 0)
                        break;
                }
                catch
                {
                    break;
                }
                

                Console.WriteLine($"{page}-ая страница выкачана");

                lastPage = page;
                htmlDocs.Add(htmlDoc);
            }

            var cars = htmlDocs
                .Select(x => x.DocumentNode
                    .SelectNodes("//tr[@id]")
                    .Cast<HtmlNode>()
                    .Aggregate(new List<FgisTaxiRecord>(), (acc, cur) => 
                    {
                        var tds = cur.SelectNodes("td");

                        return [..acc, new FgisTaxiRecord
                        {
                            Guid = cur.Id,
                            RegionName = tds[0].SelectSingleNode("a").InnerText.Trim(),
                            RecordNumber = tds[1].SelectSingleNode("a").InnerText.Trim(),
                            RecordDate = DateTime.ParseExact(tds[2].SelectSingleNode("a").InnerText.Trim(), "dd.MM.yyyy", null),
                            GosNumber = tds[3].SelectSingleNode("a").InnerText.Trim(),
                            VehicleMark = tds[4].SelectSingleNode("a").InnerText.Trim(),
                            VehicleModel = tds[5].SelectSingleNode("a").InnerText.Trim(),
                            Status = tds[6].InnerText.Trim(),
                        }];
                    }))
                .Aggregate(new List<FgisTaxiRecord>(), (acc, cur) => [.. acc, .. cur]);

            await stfDbContext.TaxiCars.AddRangeAsync(cars);
            await stfDbContext.SaveChangesAsync();

            Console.WriteLine($"имеющиеся данные для страниц с {firstPage} по {lastPage} сохранены в БД");

            return lastPage;
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

            using var fs = File.Create( $"{env.ContentRootPath}\\captcha.png");
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
