using System.Security.Cryptography.X509Certificates;

namespace StateTrafficPoliceApi.StfDtos.Auto
{
    public class StfAutoResponseDTO
    {
        public string RequestTime { get; set; }

        public StfAutoRequestResult RequestResult { get; set; }

        public string Hostname { get; set; }

        public string Vin { get; set; }

        public string Regnum { get; set; }

        public string Message { get; set; }

        public string RegisterToken { get; set; }

        public int Status { get; set; }
    }
}
