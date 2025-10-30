using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_project.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string CategoryName { get; set; }

        public int? UserId { get; set; } //Якщо UserId == null, це загальна категорія.

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
