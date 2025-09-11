namespace csms_backend.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Status Status { get; set; }
        public ICollection<Representative> Representative { get; set; }
        public ICollection<Product> Product { get; set; }
    }
    public class Representative
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public Status Status { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
