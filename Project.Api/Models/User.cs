using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public int RoleId { get; set; }

        public Role? Role { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }
    }
}
