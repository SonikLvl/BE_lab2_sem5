using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_project.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }
    }
}
