using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace StateTrafficPoliceApi.IdxDtos.Driver
{
    public class IdxDecisionDTO
    {
        public string State { get; set; }

        public string Date { get; set; }

        public string Period { get; set; }

        public string BirthPlace { get; set; }

        public string RegName { get; set; }

        public string Comment { get; set; }
    }
}
