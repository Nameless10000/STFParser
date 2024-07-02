using StateTrafficPoliceApi.StfDtos;

namespace StateTrafficPoliceApi.Dtos
{
    public class CapchaDTO : StfCapchaDTO
    {
        public string CapchaWord { get; set; }

        internal static CapchaDTO FromStf(StfCapchaDTO stfCapcha, string capchaWord)
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
