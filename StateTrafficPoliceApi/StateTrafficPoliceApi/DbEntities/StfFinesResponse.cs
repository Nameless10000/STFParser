using System.ComponentModel.DataAnnotations;

namespace StateTrafficPoliceApi.DbEntities
{
    public class StfFinesResponse
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Data { get; set; }

        public string Gosnomer { get; set; }

        public string Sts { get; set; }
    }
}
