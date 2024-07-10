using System.ComponentModel.DataAnnotations;

namespace StateTrafficPoliceApi.Dtos.Driver
{
    public class DrivingLicenseCheckDTO
    {
        public string drivingLicenseNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string drivingLicenseDate { get; set; }
    }
}
