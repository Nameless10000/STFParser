namespace StateTrafficPoliceApi.IdxDtos.AutoHistory
{
    public class IdxAutoHistoryDTO
    {
        public int Status { get; set; } = 0;

        public IdxVehicle Vehicle { get; set; }

        /*public VehiclePassport VehiclePassport { get; set; }*/

        public List<IdxOwnershipPeriod> OwnershipPeriods { get; set; }
    }
}
