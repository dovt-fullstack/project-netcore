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
}
