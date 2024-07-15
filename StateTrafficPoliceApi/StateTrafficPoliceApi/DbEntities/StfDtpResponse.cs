using System.ComponentModel.DataAnnotations;

namespace StateTrafficPoliceApi.DbEntities
{
    public class StfDtpResponse
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Data { get; set; }

        public string Vin { get; set; }
    }
}
