namespace BE_project.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
    }
}
