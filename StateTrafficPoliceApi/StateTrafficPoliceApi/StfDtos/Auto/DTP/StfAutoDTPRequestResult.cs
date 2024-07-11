namespace StateTrafficPoliceApi.StfDtos.Auto.DTP
{
    public class StfAutoDTPRequestResult
    {
        public string ErrorDescription { get; set; }

        public int StatusCode { get; set; }

        public List<StfAccidentDTO> Accidents { get; set; }
    }
}