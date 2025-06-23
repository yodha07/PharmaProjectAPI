namespace PharmaProject.Models
{
    public class MedicineStockDto
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int TotalQuantity { get; set; }
        public decimal CostPrice { get; set; }
    }
}
