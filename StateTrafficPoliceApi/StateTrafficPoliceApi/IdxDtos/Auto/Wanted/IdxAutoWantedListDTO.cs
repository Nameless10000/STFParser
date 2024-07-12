namespace StateTrafficPoliceApi.IdxDtos.Auto.Wanted
{
    public class IdxAutoWantedListDTO
    {
        public int Status { get; set; } = 0;

        public List<IdxAutoWantedDTO> WantedList { get; set; }
    }
}
