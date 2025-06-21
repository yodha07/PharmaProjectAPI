namespace PharmaProjectAPI.DTO
{
    public class PurchaseItemDTO
    {
        //public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public string CreatedBy { get; set; }
    }
}
