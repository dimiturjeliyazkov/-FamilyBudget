using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Entities
{
    public class Transaction
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string? Category { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public DateTime ModifiedTime { get; set; }

        public string? Description { get; set; }

        public int RepeatDay { get; set; }
    }
}
