namespace PharmaProjectAPI.Models
{
    public class PurchaseMedDTO
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public decimal PricePerUnit { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
