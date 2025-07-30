namespace CSMapi.Models
{
    public class CustomerRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string Companyname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Companyaddress { get; set; }
        public string Companyemail { get; set; }
        public string Companynumber { get; set; }
    }

    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string Companyname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Companyaddress { get; set; }
        public string Companyemail { get; set; }
        public string Companynumber { get; set; }
        public bool Active { get; set; }
        public bool Removed { get; set; }
    }

    public class CompanyNameOnlyResponse
    {
        public int Id { get; set; }
        public string Companyname { get; set; }
    }
}
