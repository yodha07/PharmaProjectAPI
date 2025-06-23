namespace PharmaProject.Models
{
    public class OSaleItemDTO2
    {
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public string mname { get; set; }
        public int PurchaseItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
