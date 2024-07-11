namespace StateTrafficPoliceApi.StfDtos.Auto.DiagnosticCard
{
    public class StfAutoDcDTO
    {
        public DateTime DcExpirationDate { get; set; }

        public string PointAddress { get; set; }

        public string Chassis { get; set; }

        public string Body { get; set; }

        public string OperatorName { get; set; }

        public string OdometerValue { get; set; }

        public string DcNumber { get; set; }

        public DateTime DcDate { get; set; }

        public List<StfAutoShortDcDTO> PreviousDcs { get; set; }

        public string Vin { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }
    }
}