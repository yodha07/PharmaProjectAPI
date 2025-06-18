namespace PharmaProjectAPI.DTO
{
    public class SalesReportDTO
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
