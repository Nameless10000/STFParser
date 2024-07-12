namespace StateTrafficPoliceApi.Dtos.Auto
{
    public class AutoResolvedVinDTO
    {
        public string Vin { get; set; }

        public string CheckType { get; set; }

        public string CaptchaToken { get; set; }

        public string CaptchaWord { get; set; }

        public static AutoResolvedVinDTO FromCheck(AutoCheckVinDTO autoCheckDTO, CaptchaDTO captchaDTO, string checkType)
        {
            return new()
            {
                Vin = autoCheckDTO.Vin,
                CheckType = checkType,
                CaptchaToken = captchaDTO.Token,
                CaptchaWord = captchaDTO.CaptchaWord
            };
        }
    }
}
