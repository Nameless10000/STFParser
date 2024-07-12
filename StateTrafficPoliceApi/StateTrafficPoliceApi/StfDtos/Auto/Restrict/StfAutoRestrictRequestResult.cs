namespace StateTrafficPoliceApi.StfDtos.Auto.Restrict
{
    public class StfAutoRestrictRequestResult
    {
        public List<StfAutoRestrictDTO> Records { get; set; }

        public int Count { get; set; }

        public int Error { get; set; }
    }
}