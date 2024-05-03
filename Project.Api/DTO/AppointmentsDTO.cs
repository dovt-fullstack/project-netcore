namespace Project.Api.DTO
{
    public class AppointmentsDTO
    {
    }
    public class CreateAppointmentsDTO
    {
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public int ClinicID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public List<string> ServiceIDs { get; set; } 
    }
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string UserName { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public List<string> ServiceNames { get; set; }
    }
    public class UpdateAppointmentDTO
    {
        public string status { get; set; }
    }
}
