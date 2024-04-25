using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class Services
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public ICollection<Appointments> Appointments { get; set; }
    }
}
