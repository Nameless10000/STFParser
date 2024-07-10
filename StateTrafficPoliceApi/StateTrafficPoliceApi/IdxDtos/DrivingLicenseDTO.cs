using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.IdxDtos
{
    public class DrivingLicenseDTO
    {
        public string ResultMessage { get; set; }

        public int ResultCode { get; set; }

        public string PersonBirthDate { get; set; }

        public string DrivingLicenseNumber { get; set; }

        public string DrivingLicenseIssueDate { get; set; }

        public string DrivingLicenseExpiryDate { get; set; }

        public string DrivingLicenseCategory { get; set; }

        public List<object> DecisionList { get; set; }
    }
}
