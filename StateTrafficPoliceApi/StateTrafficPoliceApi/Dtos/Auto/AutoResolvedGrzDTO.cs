namespace StateTrafficPoliceApi.Dtos.Auto
{
    public class AutoResolvedGrzDTO
    {
        public string Regnum { get; set; }

        public string Regreg { get; set; }

        public string Stsnum { get; set; }

        public string CaptchaWord { get; set; }

        public string CaptchaToken { get; set; }

        public static AutoResolvedGrzDTO FromCheck(AutoCheckGrzDTO autoCheckGrzDTO, CaptchaDTO captchaDTO)
        {
            var grzData = autoCheckGrzDTO.Gosnomer.Split(" ");
            return new AutoResolvedGrzDTO
            {
                Regnum = grzData[0],
                Regreg = grzData[1],
                Stsnum = autoCheckGrzDTO.Sts,
                CaptchaToken = captchaDTO.Token,
                CaptchaWord = captchaDTO.CaptchaWord
            };
        }
    }
}
