namespace PharmaProjectAPI.DTO
{
    public class PurchaseItemDtoSF
    {
        public int PurchaseItemId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
    }
}
