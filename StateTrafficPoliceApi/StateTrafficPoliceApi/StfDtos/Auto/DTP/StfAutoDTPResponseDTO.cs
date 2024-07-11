namespace StateTrafficPoliceApi.StfDtos.Auto.DTP
{
    public class StfAutoDTPResponseDTO
    {
        public string RequestTime { get; set; }

        public StfAutoDTPRequestResult RequestResult { get; set; }

        public string Hostname { get; set; }

        public string Vin { get; set; }

        public int Status { get; set; }
    }
}
