using System.ComponentModel.DataAnnotations;

namespace StateTrafficPoliceApi.DbEntities
{
    public class StfDriverLicenseResponse
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Data { get; set; }

        public string DrivingLicenseNumber { get; set; }

        public string drivingLicenseDate { get; set; }
    }
}
