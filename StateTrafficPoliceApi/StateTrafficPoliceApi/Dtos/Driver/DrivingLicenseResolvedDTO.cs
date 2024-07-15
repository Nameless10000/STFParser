namespace StateTrafficPoliceApi.Dtos.Driver
{
    public class DrivingLicenseResolvedDTO
    {
        public string Num { get; set; }

        public string Date { get; set; }

        public string CaptchaToken { get; set; }

        public string CaptchaWord { get; set; }

        internal static DrivingLicenseResolvedDTO FromCheck(DrivingLicenseCheckDTO checkDTO, CaptchaDTO capchaDTO)
        {
            return new()
            {
                Num = checkDTO.DrivingLicenseNumber,
                Date = checkDTO.DrivingLicenseDate,
                CaptchaToken = capchaDTO.Token,
                CaptchaWord = capchaDTO.CaptchaWord
            };
        }
    }
}
