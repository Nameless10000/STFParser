using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.StfDtos.Auto.History
{
    public class StfAutoHistoryRequestResult
    {
        [JsonPropertyName("vehicle_chassisnumber")]
        public string VehicleChassIsNumber { get; set; }

        [JsonPropertyName("vehicle_eco_class")]
        public string VehicleEcoClass { get; set; }

        [JsonPropertyName("cyear")]
        public string CurrentYear { get; set; }

        [JsonPropertyName("cmonth")]
        public string CurrentMonth { get; set; }

        [JsonPropertyName("cday")]
        public string CurrentDay { get; set; }

        [JsonPropertyName("chour")]
        public string CurrentHour { get; set; }

        [JsonPropertyName("cminute")]
        public string CurrentMinute { get; set; }

        [JsonPropertyName("vehicle_type_name")]
        public string VehicleBodyType { get; set; }

        [JsonPropertyName("vehicle_brandmodel")]
        public string VehicleBrandmodel { get; set; }

        [JsonPropertyName("vehicle_enclosedvolume")]
        public string VehicleEnclosedVolume { get; set; }

        [JsonPropertyName("vehicle_releaseyear")]
        public string VehicleReleaseYear { get; set; }

        [JsonPropertyName("vehicle_vin")]
        public string VehicleVin { get; set; }

        [JsonPropertyName("vehicle_enginepowerkw")]
        public string VehicleEnginePowerKW { get; set; }

        [JsonPropertyName("vehicle_enginepower")]
        public string VehicleEnginePower { get; set; }

        public List<StfAutoPeriod> Periods { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("vehicle_bodycolor")]
        public string VehicleBodyColor { get; set; }

        [JsonPropertyName("vehicle_body_number")]
        public string VehicleBodyNumber { get; set; }

        [JsonPropertyName("reestr_status")]
        public string ReestrStatus { get; set; }
    }
}