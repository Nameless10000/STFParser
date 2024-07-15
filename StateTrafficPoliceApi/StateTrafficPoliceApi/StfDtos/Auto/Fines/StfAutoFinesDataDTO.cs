using System.Text.Json.Nodes;

namespace StateTrafficPoliceApi.StfDtos.Auto.Fines
{
    public class StfAutoFinesDataDTO
    {
        public JsonObject Divisions { get; set; }

        public string Discount { get; set; }

        public bool EnebleDiscount { get; set; }

        public string DateDecis { get; set; }

        public string KoAPcode { get; set; }

        public string DateDiscount { get; set; }

        public string VehicleModel { get; set; }

        public string KoAPtext { get; set; }

        public string NumPost { get; set; }

        public string Kbk { get; set; }

        public double Summa { get; set; }

        public int Division { get; set; }

        public bool EnablePics { get; set; }

        public string Id { get; set; }

        public string SupplierBillID { get; set; }

        public DateTime DatePost { get; set; }

        public DateTime? DateSSP { get; set; }

        public List<string> Photos { get; set; }
    }
}