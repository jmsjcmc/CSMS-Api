namespace csms_backend.Models
{
    public class PalletResponse
    {
        public int Id { get; set; }
        public string PalletType { get; set; }
        public int? PalletNo { get; set; }
        public string CFirstName { get; set; } // User
        public string CLastName { get; set; } // User
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Status Status { get; set; }
    }
    public class PalletRequest
    {
        public string PalletType { get; set; }
        public int? PalletNo { get; set; }
    }
    public class PalletPositionResponse
    {
        public string CsNumber { get; set; } // Cold Storage
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string Column { get; set; }
        public string Side { get; set; }
        public Status Status { get; set; }
    }
    public class PalletPositionRequest
    {
        public int CsId { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string Column { get; set; }
        public string Side { get; set; }
    }
    public class ColdStorageResponse
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public Status Status { get; set; }
    }
    public class ColdStorageRequest
    {
        public string Number { get; set; }
    }
}
