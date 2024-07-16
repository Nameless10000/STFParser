using System.Numerics;
using System.Text;
using System.Text.Json.Nodes;

namespace StateTrafficPoliceApi.StfDtos.Auto.Fines
{
    public class StfAutoFinesResponseDTO : AbstractResponseDTO
    {
        public int DurationReg { get; set; }

        public string Request { get; set; }

        public int Code { get; set; }

        public override int Status => Code;

        public List<StfAutoFinesDataDTO> Data { get; set; }

        public DateTime EndDate { get; set; }

        public string CafapPicsToken { get; set; }

        public JsonObject Divisions { get; set; }

        public string RequestTime { get; set; }

        public int Duration { get; set; }

        public string Hostname { get; set; }

        public string MessageReg { get; set; }

        public DateTime StartDate { get; set; }
    }
}
