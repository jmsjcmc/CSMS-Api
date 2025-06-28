namespace CSMapi.Models
{
    public class PalletRequest
    {
        public int Positionid { get; set; }
        public string Pallettype { get; set; }
        public int Palletno { get; set; }
    }

    public class PalletResponse
    {
        public int Id { get; set; }
        public string Pallettype { get; set; }
        public int Palletno { get; set; }
        public bool Occupied { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public UserEsignResponse User { get; set; }
    }

    public class ActivePalletResponse
    {
        public int Id { get; set; }
        public int Palletno { get; set; }
    }

    public class OccupiedPalletResponse
    {
        public int Id { get; set; }
        public int Palletno { get; set; }
        public ProductOnlyResponse Product { get; set; }
        public List<RepalletizeDetailResponse> ReceivingDetail { get; set; }
    }

    public class ProductBasedOccupiedPalletResponse
    {
        public int Id { get; set; }
        public int Palletno { get; set; }
    }
    public class PalletOnlyResponse
    {
        public int Id { get; set; }
        public string Pallettype { get; set; }
        public int Palletno { get; set; }
        public bool Occupied { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }

    public class PalletPositionRequest
    {
        public int Csid { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string Column { get; set; }
        public string Side { get; set; }
    }

    public class PalletPositionResponse
    {
        public int Id { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string Column { get; set; }
        public string Side { get; set; }
        public bool Hidden { get; set; }
        public ColdStorageResponse ColdStorage { get; set; }
    }

    public class ColdStorageRequest
    {
        public string Csnumber { get; set; }
    }

    public class ColdStorageResponse
    {
        public int Id { get; set; }
        public string Csnumber { get; set; }
        public bool Active { get; set; }
    }

    public class RepalletizationRequest
    {
        public int Frompalletid { get; set; }
        public int Topalletid { get; set; }
        public List<RepalletizationDetailRequest> RepalletizationDetail { get; set; } = new List<RepalletizationDetailRequest>();
    }

    public class RepalletizationDetailRequest
    {
        public int Receivingdetailid { get; set; }
        public int Quantitymoved { get; set; }
    }
}
