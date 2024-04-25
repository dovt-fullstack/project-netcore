namespace Project.Api.DTO
{
    public class Specialty
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
}
