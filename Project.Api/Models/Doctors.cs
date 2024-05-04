using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Api.Models
{
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set;}

        [ForeignKey("Specialties")]
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string Email { get; set; }
        public string Schedule { get; set; }
        public string Password { get; set; }
        public Specialties Specialty { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }
    }

}
