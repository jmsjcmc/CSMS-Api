namespace csms_backend.Models
{
    public class Pallet
    {
        public int Id { get; set; }
        public string PalletType { get; set; }
        public int? PalletNo { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Status Status { get; set; }
    }
    public class PalletPosition
    {
        public int Id { get; set; }
        public int CsId { get; set; }
        public ColdStorage ColdStorage { get; set; }
        public string Wing { get; set; }
        public string Floor { get; set; }
        public string Column { get; set; }
        public string Side { get; set; }
        public Status Status { get; set; }
    }
    public class ColdStorage
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public Status Status { get; set; }
        public ICollection<PalletPosition> PalletPosition { get; set; }
    }
}
