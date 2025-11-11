using System.ComponentModel.DataAnnotations;

namespace BE_project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
