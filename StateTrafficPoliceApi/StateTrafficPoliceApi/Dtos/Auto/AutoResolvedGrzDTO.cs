using System.Text.RegularExpressions;

namespace StateTrafficPoliceApi.Dtos.Auto
{
    public partial class AutoResolvedGrzDTO
    {
        public string Regnum { get; set; }

        public string? Regreg { get; set; }

        public string Stsnum { get; set; }

        public string CaptchaWord { get; set; }

        public string CaptchaToken { get; set; }

        public bool? Alien { get; set; }

        public static AutoResolvedGrzDTO FromCheck(AutoCheckGrzDTO autoCheckGrzDTO, CaptchaDTO captchaDTO)
        {
            var matches = GrzRegex().Match(autoCheckGrzDTO.Gosnomer);

            return matches.Groups.Count == 2
                ? new AutoResolvedGrzDTO
                {
                    Regnum = matches.Groups[1].Value,
                    Regreg = matches.Groups[2].Value,
                    Stsnum = autoCheckGrzDTO.Sts,
                    CaptchaToken = captchaDTO.Token,
                    CaptchaWord = captchaDTO.CaptchaWord
                }
                : new AutoResolvedGrzDTO
                {
                    Regnum = autoCheckGrzDTO.Gosnomer,
                    Stsnum = autoCheckGrzDTO.Sts,
                    Alien = true,
                    CaptchaToken = captchaDTO.Token,
                    CaptchaWord = captchaDTO.CaptchaWord
                };
        }

        [GeneratedRegex(@"([a-z,A-Z,А-Я,а-я]{1}\d{3}[a-z,A-Z,А-Я,а-я]{2})(\d{2,3})")]
        private static partial Regex GrzRegex();
    }
}
