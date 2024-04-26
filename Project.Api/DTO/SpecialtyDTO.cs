namespace Project.Api.DTO
{
    public class SpecialtyDTO
    {
    }
    public class CreateSpecialtyDTO
    {
        public string SpecialtyName { get; set; }
    }
    public class PostSpecialtyResponse {
        public string SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }

    }
    public class ResponeDoctorInSpec
    {
        public int IdSpe { get; set; }
        public string NameSpe { get; set; }
        public int IdDoctor { get; set; }
        public string NameDoctor { get; set; }
        public string EmailDoctor { get; set; }
    }
}
