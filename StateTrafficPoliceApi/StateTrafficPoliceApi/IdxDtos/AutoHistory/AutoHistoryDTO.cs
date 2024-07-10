namespace StateTrafficPoliceApi.IdxDtos.AutoHistory
{
    public class AutoHistoryDTO
    {
        public int Status { get; set; } = 0;

        public Vehicle Vehicle { get; set; }

        /*public VehiclePassport VehiclePassport { get; set; }*/

        public List<OwnershipPeriod> OwnershipPeriods { get; set; }
    }
}
