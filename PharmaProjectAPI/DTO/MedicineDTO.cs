namespace PharmaProjectAPI.DTO
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public decimal PricePerUnit { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
