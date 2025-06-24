namespace PharmaProject.Models
{
    public class ExpenseDTO
    {
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
