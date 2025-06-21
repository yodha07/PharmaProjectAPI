namespace PharmaProjectAPI.DTO
{
    public class PurchaseItemDTO2
    {
        public int PurchaseItemId { get; set; }
        public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
        public string Mname { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
