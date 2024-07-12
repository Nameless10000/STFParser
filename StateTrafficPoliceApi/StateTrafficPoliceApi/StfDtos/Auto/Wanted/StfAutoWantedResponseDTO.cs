namespace StateTrafficPoliceApi.StfDtos.Auto.Wanted
{
    public class StfAutoWantedResponseDTO
    {
        public string RequestTime { get; set; }

        public StfAutoWantedRequestResult RequestResult { get; set; }

        public string Hostname { get; set; }

        public string Vin { get; set; }

        public int Status { get; set; }
    }
}
