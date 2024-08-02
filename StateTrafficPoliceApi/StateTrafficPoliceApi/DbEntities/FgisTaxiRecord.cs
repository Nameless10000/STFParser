using System.ComponentModel.DataAnnotations;

namespace StateTrafficPoliceApi.DbEntities
{
    public class FgisTaxiRecord
    {
        public string RegionName { get; set; }

        public string RecordNumber { get; set; }

        public DateTime RecordDate { get; set; }

        public string GosNumber { get; set; }

        public string VehicleMark { get; set; }

        public string VehicleModel { get; set; }

        public string Status { get; set; }

        [Key]
        public string Guid { get; set; }
    }
}
