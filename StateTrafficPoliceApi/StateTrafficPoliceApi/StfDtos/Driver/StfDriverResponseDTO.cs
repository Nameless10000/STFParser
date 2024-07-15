namespace StateTrafficPoliceApi.StfDtos.Driver;

public class StfDriverResponseDTO : AbstractResponseDTO
{
    public string RequestTime { get; set; }

    public string Hostname { get; set; }

    public int Code { get; set; }

    public int Count { get; set; }

    public StfDocDTO Doc { get; set; }

    public List<StfDecisionDTO> Decis { get; set; }
}
