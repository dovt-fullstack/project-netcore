using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Project.Api.Models
{
    public class MedicalRecords
    {
        [Key]   
        public int RecordID { get; set; }

        // Foreign keys
        public int UserID { get; set; }
        public int DoctorID { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        // Navigation properties for related entities
        public User User { get; set; }

        public Doctors Doctor { get; set; }

    }
}
