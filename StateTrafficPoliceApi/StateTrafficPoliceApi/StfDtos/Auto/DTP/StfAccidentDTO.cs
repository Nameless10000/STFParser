namespace StateTrafficPoliceApi.StfDtos.Auto.DTP
{
    public class StfAccidentDTO
    {
        public string AccidentDateTime { get; set; }

        public string VehicleDamageState { get; set; }

        public string AccidentNumber { get; set; }

        public string AccidentType {get; set;}

        public string DamageDestription {get; set;}

        public string VehicleMark {get; set;}

        public string VehicleAmount {get; set;}

        public string VehicleYear {get; set;}

        public string AccidentPlace {get; set;}

        public string VehicleSort {get; set;}

        public string VehicleModel {get; set;}

        public string OwnerOkopf {get; set;}

        public string RegionName {get; set;}

        public List<string> DamagePoints { get; set; }
    }
}