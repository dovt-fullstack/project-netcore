using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class Doctors
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set;}
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }

        public Specialties Specialty { get; set; }

        // Navigation property for related entities
        public ICollection<Appointments> Appointments { get; set; }

        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }

    }

}
