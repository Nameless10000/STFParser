namespace StateTrafficPoliceApi.StfDtos.Auto.Restrict
{
    public class StfAutoRestrictResponseDTO : AbstractResponseDTO
    {
        public string RequestTime { get; set; }

        public StfAutoRestrictRequestResult RequestResult {get;set;}

        public string Hostname { get; set; }

        public string Vin { get; set; }

        public int Status { get; set; }
    }
}
