namespace CSMapi.Models
{

    public class UserRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string Businessunit { get; set; }
        public string Businessunitlocation { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
    public class UserResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Businessunit { get; set; }
        public string Businessunitlocation { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Esignature { get; set; }
        public DateTime Createdon { get; set; }
        public DateTime Updatedon { get; set; }
        public bool Removed { get; set; }
    }

    public class UserEsignResponse
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string Esignature { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RoleRequest
    {
        public string Rolename { get; set; }
    }

    public class RoleResponse
    {
        public int Id { get; set; }
        public string Rolename { get; set; }
        public bool Removed { get; set; }
    }
}
