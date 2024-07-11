namespace StateTrafficPoliceApi.StfDtos.Auto.DiagnosticCard;

public class StfAutoDCResponseDTO
{
    public string RequestTime { get; set; }

    public StfAutoDcRequestResult RequestResult { get; set; }

    public string Hostname { get; set; }

    public string Vin { get; set; }

    public int Status { get; set; }
}