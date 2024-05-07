namespace Project.Api.DTO
{
    public class EvaluationDTO
    {
        public int idEvaluation { get; set; }
        public string User { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public int  appointment { get; set; }
    }
    public class CreateEvaluationDTO
    {
        public int AppointmentsId { get; set; }
        public int UserID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
    }
    public class EditEval
    {
        public int Star { get; set; }
        public string Content { get; set; }
    }
}
