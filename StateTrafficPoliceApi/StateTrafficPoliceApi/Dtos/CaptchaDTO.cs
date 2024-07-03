using StateTrafficPoliceApi.StfDtos;

namespace StateTrafficPoliceApi.Dtos
{
    public class CaptchaDTO : StfCaptchaDTO
    {
        public string CapchaWord { get; set; }

        internal static CaptchaDTO FromStf(StfCaptchaDTO stfCapcha, string capchaWord)
        {
            return new()
            {
                Base64jpg = stfCapcha.Base64jpg,
                Token = stfCapcha.Token,
                CapchaWord = capchaWord
            };
        }
    }
}
