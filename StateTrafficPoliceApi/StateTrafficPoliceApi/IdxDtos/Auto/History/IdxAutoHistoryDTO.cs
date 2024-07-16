namespace StateTrafficPoliceApi.IdxDtos.Auto.History
{
    public class IdxAutoHistoryDTO : IdxAbstractResposeDTO
    {
        public IdxVehicle Vehicle { get; set; }

        /*public VehiclePassport VehiclePassport { get; set; }*/

        public List<IdxOwnershipPeriod> OwnershipPeriods { get; set; }
    }
}
