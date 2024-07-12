namespace StateTrafficPoliceApi.StfDtos.Auto.Wanted
{
    public class StfAutoWantedRequestResult
    {
        public int Count { get; set; }

        public int Error { get; set; }

        public List<StfAutoWantedDTO> Records { get; set; }
    }
}