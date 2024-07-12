using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.StfDtos.Auto.Wanted
{
    public class StfAutoWantedDTO
    {
        [JsonPropertyName("w_rec")]
        public int RecordNumber { get; set; }

        [JsonPropertyName("w_reg_inic")]
        public string RegionIniciator { get; set; }

        [JsonPropertyName("w_dvig")]
        public string Engine { get; set; }

        [JsonPropertyName("w_kuzov")]
        public string Body { get; set; }

        [JsonPropertyName("w_model")]
        public string Model { get; set; }

        [JsonPropertyName("w_reg_zn")]
        public string Grz { get; set; }

        [JsonPropertyName("w_shassi")]
        public string Chassis { get; set; }

        [JsonPropertyName("w_data_pu")]
        public string PermanentAccountingDate { get; set; }

        [JsonPropertyName("w_vin")]
        public string Vin { get; set; }

        [JsonPropertyName("w_god_vyp")]
        public string VehicleYear { get; set; }

        [JsonPropertyName("w_vid_uch")]
        public string AccountingType { get; set; }

        [JsonPropertyName("w_un_gic")]
        public string UnGic { get; set; }
    }
}