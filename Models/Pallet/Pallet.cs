namespace CSMapi.Models
{
    public class Pallet
    {
        public int Id { get; set; }
        public string Taggingnumber { get; set; }
        public string Pallettype { get; set; }
        public int? Palletno { get; set; }
        public int Creatorid { get; set; }
        public User Creator { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public bool Occupied { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
        public ICollection<ReceivingDetail> ReceivingDetail { get; set; }
        public ICollection<DispatchingDetail> DispatchDetail { get; set; }
    }

    public class PalletPosition
    {
        public int Id { get; set; }
        public int Csid { get; set; }
        public ColdStorage Coldstorage { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string? Column { get; set; }
        public string? Side { get; set; }
        public bool Hidden { get; set; }
        public bool Removed { get; set; }
        public ICollection<ReceivingDetail> ReceivingDetail { get; set; }
        public ICollection<DispatchingDetail> DispatchingDetail { get; set; }
    }

    public class ColdStorage
    {
        public int Id { get; set; }
        public string Csnumber { get; set; }
        public bool Active { get; set; }
        public ICollection<PalletPosition> Palletposition { get; set; }
    }

    public class Repalletization
    {
        public int Id { get; set; }
        public int Frompalletid { get; set; }
        public int Topalletid { get; set; }
        public DateTime Createdon { get; set; }
        public int Creatorid { get; set; }
        public User Creator { get; set; }
        public ICollection<RepalletizationDetail> RepalletizationDetail { get; set; }
    }

    public class RepalletizationDetail
    {
        public int Id { get; set; }
        public int Repalletizationid { get; set; }
        public Repalletization Repalletization { get; set; }
        public int Receivingdetailid { get; set; }
        public ReceivingDetail Receivingdetail { get; set; }
        public int Quantitymoved { get; set; }
        public double Weightmoved { get; set; }
    }
}
