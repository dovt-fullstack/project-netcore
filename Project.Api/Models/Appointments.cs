using System.Numerics;

namespace Project.Api.Models
{
    //SpecialtyID
    public class Appointments
    {
        public int AppointmentsId { get; set; }
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public int ClinicID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }

        // Navigation properties for related entities
        public User User { get; set; }

        public Doctors Doctor { get; set; }

        public Clinics Clinic { get; set; }
        public ICollection<Services> Services { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
        public ICollection<AppointmentHistory> AppointmentHistories { get; set; }
    }
}
