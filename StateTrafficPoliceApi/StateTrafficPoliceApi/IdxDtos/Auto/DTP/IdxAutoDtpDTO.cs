namespace StateTrafficPoliceApi.IdxDtos.Auto.DTP
{
    public class IdxAutoDtpDTO
    {
        public int Status { get; set; } = 0;

        public List<IdxAccidentDTO> DtpList { get; set; }
    }
}
