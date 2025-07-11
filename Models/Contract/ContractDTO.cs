namespace CSMapi.Models
{
    public class ContractRequest
    {
        public string Agreementdate { get; set; }
        public string Lessorcompany { get; set; }
        public string Lessorrepresentative { get; set; }
        public string Lessorrepresentativeposition { get; set; }
        public string Lessorcompanylocation { get; set; }
        public string Lesseecompany { get; set; }
        public string Lesseerepresentative { get; set; }
        public string Lesseerepresentativeposition { get; set; }
        public string Lesseecompanylocation { get; set; }
        public DateTime Startlease { get; set; }
        public DateTime Endlease { get; set; }
        public string Notarylocation { get; set; }
        public string Lessoridtype { get; set; }
        public string Lessoriddetail { get; set; }
        public string Lesseeidtype { get; set; }
        public string Lesseeiddetail { get; set; }
        public string Sealdate { get; set; }
        public List<LeasedPremisesRequest> LeasedPremise { get; set; }
    }

    public class ContractResponse
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
        public DateTime Startlease { get; set; }
        public DateTime Endlease { get; set; }
        public string Notarylocation { get; set; }
        public string Lessoridtype { get; set; }
        public string Lessoriddetail { get; set; }
        public string Lesseeidtype { get; set; }
        public string Lesseeiddetail { get; set; }
        public string Sealdate { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public bool Removed { get; set; }
        public UserEsignResponse User { get; set; }
        public List<LeasedPremisesResponse> LeasedPremise { get; set; }
    }

    public class LeasedPremisesRequest
    {
        public int Contractid { get; set; }
        public string Facility { get; set; }
        public string Specific { get; set; }
    }

    public class LeasedPremisesResponse
    {
        public int Id { get; set; }
        public string Facility { get; set; }
        public string Specific { get; set; }
    }
}
