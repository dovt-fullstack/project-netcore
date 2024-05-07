using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Api.Models
{
    public class Evaluation
    {
        [Key]
        public int IdEvaluation { get; set; }

        public int UserID { get; set; }
        public int Star { get; set; }
        public string Content { get; set; }
        public int AppointmentsId { get; set; }
        public Appointments Appointments { get; set; }
        public User User { get; set; }


    }
}
