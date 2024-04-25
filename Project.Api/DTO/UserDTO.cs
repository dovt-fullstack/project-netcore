namespace Project.Api.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; } // Change from int to string
        public string RoleName { get; set; }
    }
    public class UserUpdateDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
    }
    public class UpdatePassWorduserDTO
    {
        public string PassWord { get; set; }
    }

    public class CreateUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
    }
}
