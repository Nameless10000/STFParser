namespace StateTrafficPoliceApi.StfDtos
{
    public class StfCapchaDTO
    {
        public string Token { get; set; }

        public string Base64jpg { get; set; }

        public byte[] Bytes => Convert.FromBase64String(Base64jpg);
    }
}
