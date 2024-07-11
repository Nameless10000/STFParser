using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.StfDtos.Driver
{
    public class StfDecisionDTO
    {
        public DateTime Date { get; set; }

        [JsonPropertyName("fis_id")]
        public string FisID { get; set; }

        [JsonPropertyName("bplace")]
        public string BPlace { get; set; }

        public string Comment { get; set; }

        [JsonPropertyName("reg_name")]
        public string RegionName { get; set; }

        public string State { get; set; }

        public int Srok { get; set; }

        [JsonPropertyName("reg_code")]
        public string RegionCode { get; set; }
    }
}