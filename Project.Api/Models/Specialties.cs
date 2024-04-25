using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class Specialties
    {
        [Key]
        public int DoctorId { get; set; }

        public string SpecialtyName { get; set; }

        public ICollection<Appointments> Appointments { get; set; }

        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }
        public ICollection <Doctors> Doctors { get; set; }
}
}
