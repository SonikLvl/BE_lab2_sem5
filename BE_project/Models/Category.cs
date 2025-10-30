using System.ComponentModel.DataAnnotations;

namespace BE_project.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string CategoryName { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
