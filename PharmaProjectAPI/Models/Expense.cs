using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
