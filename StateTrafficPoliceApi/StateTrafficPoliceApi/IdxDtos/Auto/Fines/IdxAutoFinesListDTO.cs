namespace StateTrafficPoliceApi.IdxDtos.Auto.Fines
{
    public class IdxAutoFinesListDTO
    {
        public int Status { get; set; } = 0;

        public List<IdxAutoFineDTO> FinesList { get; set; }
    }
}
