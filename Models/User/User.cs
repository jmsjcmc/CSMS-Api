namespace CSMapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Businessunit { get; set; }
        public string Businessunitlocation { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
        public string? Esignature { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public bool Removed { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Rolename { get; set; }
        public bool Removed { get; set; }
    }
}
