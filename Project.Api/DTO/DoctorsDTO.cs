using Project.Api.Models;

namespace Project.Api.DTO
{
    public class DoctorsDTO
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Specialty { get; set; }


    }
    public class CreateDoctorDTO
    {
        public int SpecialtyID { get; set; }
        public string DoctorName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
    public class PostDoctorResponse
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Email { get; set; }
        public Specialties Specialty { get; set; }
    }

    //public class UpdateDoctor
    //{
    //    public string DoctorName { get; set; }

    //}
}
