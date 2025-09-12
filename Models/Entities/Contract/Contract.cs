namespace csms_backend.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string AgreementDate { get; set; }
        public int LessorId { get; set; }
        public Company LessorCompany { get; set; }
        public int LesseeId { get; set; }
        public Company LesseeCompany { get; set; }
        public string StartLease { get; set; }
        public string EndLease { get; set; }
        public string NotaryLocation { get; set; } 
    }
}
