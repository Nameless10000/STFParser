namespace StateTrafficPoliceApi.StfDtos.Driver;

public class StfDriverResponseDTO
{
    public string RequestTime { get; set; }

    public string Hostname { get; set; }

    public int Code { get; set; }

    public int count { get; set; }

    public DocDTO Doc { get; set; }

    public string Message { get; set; }

    public List<object> Decis { get; set; }
}
