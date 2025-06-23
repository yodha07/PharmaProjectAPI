namespace PharmaProject.Models
{
    public class TodaySaleDto
    {
        public string CustomerName { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string MedicineName { get; set; }
    }
}
