namespace StateTrafficPoliceApi.Dtos
{
    public class DrivingLicenseResolvedDTO
    {
        public string Num { get; set; }

        public string Date { get; set; }

        public string CapchaToken { get; set; }

        public string CapchaWord { get; set; }

        internal static DrivingLicenseResolvedDTO FromCheck(DrivingLicenseCheckDTO checkDTO, CapchaDTO capchaDTO)
        {
            return new()
            {
                Num = checkDTO.Num,
                Date = checkDTO.Date,
                CapchaToken = capchaDTO.Token,
                CapchaWord = capchaDTO.CapchaWord
            };
        }
    }
}
