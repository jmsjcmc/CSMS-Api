namespace CSMapi.Models
{
    public class DispatchingRequest
    {
        public int Productid { get; set; }
        public string Documentno { get; set; }
        public DateTime? Dispatchdate { get; set; }
        public string Nmiscertificate { get; set; }
        public string Dispatchplateno { get; set; }
        public string Sealno { get; set; }
        public double Overallweight { get; set; }
        public List<DispatchingDetailRequest> DispatchingDetail { get; set; }
    }

    public class DispatchingResponse
    {
        public int Id { get; set; }
        public DateTime Dispatchdate { get; set; }
        public string Dispatchtimestart { get; set; }
        public string Dispatchtimeend { get; set; }
        public string Nmiscertificate { get; set; }
        public string Dispatchplateno { get; set; }
        public string Sealno { get; set; }
        public double Overallweight { get; set; }
        public DateTime? Createdon { get; set; }
        public DateTime? Approvedon { get; set; }
        public DateTime? Updatedon { get; set; }
        public bool Dispatched { get; set; }
        public bool Pending { get; set; }
        public bool Declined { get; set; }
        public bool Removed { get; set; }
        public DocumentResponse Document { get; set; }
        public ProductResponse Product { get; set; }
        public UserEsignResponse Requestor { get; set; }
        public UserEsignResponse Approver { get; set; }
        public List<DispatchingDetailResponse> DispatchingDetail { get; set; }
    }

    public class DispatchingDetailRequest
    {
        public int Receivingdetailid { get; set; }
        public int Positionid { get; set; }
        public int Palletid { get; set; }
        public DateTime? Productiondate { get; set; }
        public int Quantity { get; set; }
        public double? Totalweight { get; set; }
    }

    public class DispatchingDetailResponse
    {
        public int Id { get; set; }
        public int Receivingdetailid { get; set; }
        public int Quantity { get; set; }
        public DateTime? Productiondate { get; set; }
        public double Totalweight { get; set; }
        public PalletResponse Pallet { get; set; }
        public PalletPositionResponse Position { get; set; }
    }
    public class ProductBasedDispatchingDetailResponse
    {
        public int Id { get; set; }
        public int Receivingdetailid { get; set; }
        public int Quantity { get; set; }
        public DateTime? Productiondate { get; set; }
        public double Totalweight { get; set; }
        public PalletResponse Pallet { get; set; }
        public PalletPositionResponse Position { get; set; }
    }
    public class DispatchingTimeStartEndResponse
    {
        public string Dispatchtimestart { get; set; }
        public string Dispatchtimeend { get; set; }
    }
    public class ProductDispatchingResponse
    {
        public int Id { get; set; }
        public DateTime Dispatchdate { get; set; }
        public string Dispatchtimestart { get; set; }
        public string Dispatchtimeend { get; set; }
        public string Nmiscertificate { get; set; }
        public string Dispatchplateno { get; set; }
        public string Sealno { get; set; }
        public double Overallweight { get; set; }
        public DateTime Createdon { get; set; }
        public string Createdby { get; set; }
        public string Approvedby { get; set; }
        public DateTime Approvedon { get; set; }
        public DateTime Updatedon { get; set; }
        public string Updatedby { get; set; }
        public bool Dispatched { get; set; }
        public bool Pending { get; set; }
        public DocumentResponse Document { get; set; }
        public List<DispatchingDetailResponse> DispatchignDetail { get; set; }
    }

    public class DispatchingsCount
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Dispatched { get; set; }
        public int Declined { get; set; }
    }
}
