namespace CSMapi.Models
{
    public class PalletRequest
    {
        public string Taggingnumber { get; set; }
        public string Pallettype { get; set; }
        public int Palletno { get; set; }
    }
    public class PalletResponse
    {
        public int Id { get; set; }
        public string Taggingnumber { get; set; }
        public string Pallettype { get; set; }
        public int? Palletno { get; set; }
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
        public string Taggingnumber { get; set; }
        public string Pallettype { get; set; }
        public int? Palletno { get; set; }
        public ProductOnlyResponse Product { get; set; }
        public List<RepalletizeDetailResponse> ReceivingDetail { get; set; }
    }
    public class ProductBasedOccupiedPalletResponse
    {
        public int Id { get; set; }
        public string Taggingnumber { get; set; }
    }
    public class PalletOnlyResponse
    {
        public int Id { get; set; }
        public string Taggingnumber { get; set; }
        public string Pallettype { get; set; }
        public int? Palletno { get; set; }
        public bool Occupied { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
    public class PalletPositionRequest
    {
        public int Csid { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string? Column { get; set; }
        public string? Side { get; set; }
    }
    public class PalletPositionResponse
    {
        public int Id { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string? Column { get; set; }
        public string? Side { get; set; }
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
        public int Fromreceivingdetailid { get; set; }
        public int Toreceivingdetailtid { get; set; }
        public int Quantitymoved { get; set; }
        public double Weightmoved { get; set; }
    }
    public class RepalletizationBulkRequest
    {
        public List<RepalletizationRequest> Repalletization { get; set; }
    }
    public class RepalletizationBulkResponse
    {
        public List<RepalletizationResponse> Repalletization { get; set; }
        public int Successcount { get; set; }
        public int Failurecount { get; set; }
    }
    public class RepalletizationResponse
    {
        public int Id { get; set; }
        public int Fromreceivingdetailid { get; set; }
        public int Toreceivingdetailtid { get; set; }
        public int Quantitymoved { get; set; }
        public double Weightmoved { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Approvedon { get; set; }
        public UserEsignResponse Creator { get; set; }
        public int Status { get; set; }
    }
    public class RepalletizationDraftRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int Quantitymoved { get; set; }
        public double Weightmoved { get; set; }
        public ProductBasesPallet Fromreceivingdetail { get; set; }
        public ProductBasesPallet Toreceivingdetail { get; set; }
    }
    public class RepalletizationDraftResponse
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int Quantitymoved { get; set; }
        public double Weightmoved { get; set; }
        public ProductBasesPallet Fromreceivingdetail { get; set; }
        public ProductBasesPallet Toreceivingdetail { get; set; }
    }
    public class PalletTypeBasedResponse
    {
        public int Id { get; set; }
        public string Pallettype { get; set; }
        public string Taggingnumber { get; set; }
        public int? Palletno { get; set; }
    }
    public class PalletsCount
    {
        public int Total { get; set; }
        public int Active { get; set; }
        public int Occupied { get; set; }
        public int Repalletized { get; set; }
    }
}
