using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.StfDtos.Driver
{
    public class StfDocDTO
    {
        public DateTime Date { get; set; }

        public DateTime Bdate { get; set; }

        [JsonPropertyName("blank_srok")]
        public DateTime BlankSrok { get; set; }

        public string Num { get; set; }

        public string Index { get; set; }

        public string Type { get; set; }

        public DateTime Srok { get; set; }

        public string Cat { get; set; }

        public string Comment { get; set; }

        public long Time { get; set; }

        public string Id { get; set; }

        [JsonPropertyName("st_kart")]
        public string StKart { get; set; }

        public string Divid { get; set; }
    }
}