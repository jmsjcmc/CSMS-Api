namespace csms_backend.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Name { get; set; } // Business Unit
        public string Location { get; set; } // Business Unit
        public Status Status { get; set; }
        public List<RoleResponse> Role { get; set; } // Role
    }
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int BusinessUnitId { get; set; }
        public List<int> RoleId { get; set; }

    }
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
    }
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
    public class RoleRequest
    {
        public string Name { get; set; }
    }
    public class BusinessUnitResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Status Status { get; set; }
    }
    public class BusinessUnitRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
