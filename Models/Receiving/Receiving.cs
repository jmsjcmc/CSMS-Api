namespace CSMapi.Models
{
    public class Receiving
    {
        public int Id { get; set; }
        public int Documentid { get; set; }
        public Document Document { get; set; }
        public int Productid { get; set; }
        public Product Product { get; set; }
        public DateTime Expirationdate { get; set; }
        public string Cvnumber { get; set; }
        public string Platenumber { get; set; }
        public DateTime Arrivaldate { get; set; }
        public string Unloadingstart { get; set; }
        public string Unloadingend { get; set; }
        public double Overallweight { get; set; }
        public string? Receivingform { get; set; }
        public string? Note { get; set; }
        public int Requestorid { get; set; }
        public User Requestor { get; set; }
        public int? Approverid { get; set; }
        public User Approver { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public DateTime? Datereceived { get; set; }
        public DateTime? Datedeclined { get; set; }
        public bool Pending { get; set; }
        public bool Received { get; set; }
        public bool Declined { get; set; }
        public bool Removed { get; set; }
        public List<ReceivingDetail> Receivingdetails { get; set; } = new List<ReceivingDetail>();
    }

    public class ReceivingDetail
    {
        public int Id { get; set; }
        public int Receivingid { get; set; }
        public Receiving Receiving { get; set; }
        public int Palletid { get; set; }
        public Pallet Pallet { get; set; }
        public int Positionid { get; set; }
        public PalletPosition PalletPosition { get; set; }
        public DateTime Productiondate { get; set; }
        public int Quantityinapallet { get; set; }
        public int Duquantity { get; set; }
        public double Duweight { get; set; }
        public double Totalweight { get; set; }
        public bool Received { get; set; }
        public bool Partialdispatched { get; set; }
        public bool Fulldispatched { get; set; }
        public ICollection<DispatchingDetail> DispatchingDetail { get; set; }
        public RepalletizationDetail RepalletizationDetail { get; set; }
    }

    public class Document
    {
        public int Id { get; set; }
        public string Documentno { get; set; }
        public List<Receiving> Receiving { get; set; } = new List<Receiving>();
        public List<Dispatching> Dispatching { get; set; } = new List<Dispatching>();
    }
}
