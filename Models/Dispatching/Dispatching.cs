namespace CSMapi.Models
{
    public class Dispatching
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public Product Product { get; set; }
        public int Documentid { get; set; }
        public Document Document { get; set; }
        public DateTime Dispatchdate { get; set; }
        public string? Dispatchtimestart { get; set; }
        public string? Dispatchtimeend { get; set; }
        public string Nmiscertificate { get; set; }
        public string Dispatchplateno { get; set; }
        public string Sealno { get; set; }
        public double Overallweight { get; set; }
        public string? Note { get; set; }
        public int Requestorid { get; set; }
        public User Requestor { get; set; }
        public int? Approverid { get; set; }
        public User Approver { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? Approvedon { get; set; }
        public DateTime? Declinedon { get; set; }
        public DateTime? Updatedon { get; set; }
        public bool Dispatched { get; set; }
        public bool Declined { get; set; }
        public bool Pending { get; set; }
        public bool Removed { get; set; }
        public List<DispatchingDetail> Dispatchingdetails { get; set; } = new List<DispatchingDetail>();
    }

    public class DispatchingDetail
    {
        public int Id { get; set; }
        public int Receivingdetailid { get; set; }
        public ReceivingDetail Receivingdetail { get; set; }
        public int Dispatchingid { get; set; }
        public Dispatching Dispatching { get; set; }
        public int Palletid { get; set; }
        public Pallet Pallet { get; set; }
        public int Positionid { get; set; }
        public PalletPosition PalletPosition { get; set; }
        public DateTime? Productiondate { get; set; }
        public int Quantity { get; set; }
        public double Totalweight { get; set; }
        public bool Partialdispatched { get; set; }
        public bool Fulldispatched { get; set; }
    }
}
