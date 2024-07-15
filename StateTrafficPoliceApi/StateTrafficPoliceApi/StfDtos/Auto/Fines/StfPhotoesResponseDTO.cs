namespace StateTrafficPoliceApi.StfDtos.Auto.Fines
{
    public class StfPhotoesResponseDTO
    {
        public string RequestTime { get; set; }

        public string Hostname { get; set; }

        public string Code { get; set; }

        public string ReqToken { get; set; }

        public string Comment { get; set; }

        public List<StdPhotoDTO> Photos { get; set; }

        public string Version { get; set; }
    }
}
