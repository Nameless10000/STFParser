using System.Text.Json.Serialization;

namespace StateTrafficPoliceApi.IdxDtos.Driver
{
    public class IdxDrivingLicenseDTO : IdxAbstractResposeDTO
    {
        public string PersonBirthDate { get; set; }

        public string DrivingLicenseNumber { get; set; }

        public string DrivingLicenseIssueDate { get; set; }

        public string DrivingLicenseExpiryDate { get; set; }

        public string DrivingLicenseCategory { get; set; }

        public string Wanted { get; set; }

        public List<IdxDecisionDTO> DecisionList { get; set; }

        public string Description { get; internal set; }
    }
}
