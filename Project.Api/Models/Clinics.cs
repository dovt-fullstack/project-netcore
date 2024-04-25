using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class Clinics
    {
        [Key]
        public int ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string Phone {  get; set; }
        public string Address { get; set; }
        public ICollection<Appointments> Appointments { get; set; }

    }
}
