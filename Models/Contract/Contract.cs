using System.ComponentModel.DataAnnotations;

namespace CSMapi.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Agreementdate { get; set; }
        public string Lessorcompany { get; set; }
        public string Lessorrepresentative { get; set; }
        public string Lessorrepresentativeposition { get; set; }
        public string Lessorcompanylocation { get; set; }
        public string Lesseecompany { get; set; }
        public string Lesseerepresentative { get; set; }
        public string Lesseerepresentativeposition { get; set; }
        public string Lesseecompanylocation { get; set; }
        [Required]
        public DateTime Startlease { get; set; }
        [Required]
        public DateTime Endlease { get; set; }
        public string Notarylocation { get; set; }
        public string Lessoridtype { get; set; }
        public string Lessoriddetail { get; set; }
        public string Lesseeidtype { get; set; }
        public string Lesseeiddetail { get; set; }
        public string Sealdate { get; set; }
        public DateTime Createdon { get; set; }
        public int Creatorid { get; set; }
        public User Creator { get; set; }
        public DateTime? Updatedon { get; set; }
        public Boolean Active { get; set; }
        public Boolean Removed { get; set; }
        public List<LeasedPresmises> Leasedpremises { get; set; } = new List<LeasedPresmises>();
    }

    public class LeasedPresmises
    {
        public int Id { get; set; }
        public int Contractid { get; set; }
        public Contract Contract { get; set; }
        public string Facility { get; set; }
        public string Specific { get; set; }
    }
}
