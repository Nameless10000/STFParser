using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.IdxDtos.Driver
{
    public class IdxDrivingLicenseDTO
    {
        public int Status { get; set; } = 0;

        public string PersonBirthDate { get; set; }

        public string DrivingLicenseNumber { get; set; }

        public string DrivingLicenseIssueDate { get; set; }

        public string DrivingLicenseExpiryDate { get; set; }

        public string DrivingLicenseCategory { get; set; }

        public List<IdxDecisionDTO> DecisionList { get; set; }
    }
}
