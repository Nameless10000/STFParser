namespace StateTrafficPoliceApi.StfDtos.Auto.DiagnosticCard
{
    public class StfAutoShortDcDTO
    {
        public DateTime DcExpirationDate { get; set; }

        public string DcNumber { get; set; }

        public DateTime DcDate { get; set; }

        public string OdometerValue { get; set; }

        public string Vin { get; set; }
    }
}