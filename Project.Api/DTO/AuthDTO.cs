namespace Project.Api.DTO
{
    public class AuthDTO
    {
       
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool  IsDoctor { get; set; }
    }
    public class LoginResponse
    {
        public string Email { get; set; }
        public bool IsDoctor { get; set; }
        public string userName { get; set; }
        public int phone { get; set; }
        public string roleName { get; set; }

    }
}
