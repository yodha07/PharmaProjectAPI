namespace PharmaProject.Models
{
    public class OSaleDTO1
    {
        public DateTime SaleDate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedBy { get; set; }
    }
}
