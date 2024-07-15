namespace StateTrafficPoliceApi.IdxDtos.Auto.Fines
{
    public class IdxAutoFineDTO
    {
        public bool EnableDiscount { get; set; }

        public string DateDecis { get; set; }

        public string KoAPcode { get; set; }

        public string KoAPtext { get; set; }

        public string DateDiscount { get; set; }

        public string VehicleModel { get; set; }

        public string NumPost { get; set; }

        public string Kbk { get; set; }

        public string Summa { get; set; }

        public int Division { get; set; }

        public string DivisionName { get; set; }

        public bool EnablePics { get; set; }

        public string SupplierBillID { get; set; }

        public string DatePost { get; set; }

        public string DateSSP { get; set; }

        public List<IdxPhotoDTO> Photos { get; set; }
    }
}