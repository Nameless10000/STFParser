namespace StateTrafficPoliceApi.StfDtos
{
    public class AbstractResponseDTO
    {
        public string? Message { get; set; }

        public virtual int Status { get;set; }
    }
}
