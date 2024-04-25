using System.ComponentModel.DataAnnotations;

namespace Project.Api.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User>? User { get; set; }
    }
}
