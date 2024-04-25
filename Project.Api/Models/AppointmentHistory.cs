using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class AppointmentHistory
    {
        [Key]
        public int HistoryID { get; set; }

        public int AppointmentID { get; set; }

        public string Action { get; set; }

        public DateTime ActionDate { get; set; }

        public Appointments Appointment { get; set; }
    }
}
