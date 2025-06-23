namespace PharmaProject.Models
{
    public class OSaleItemDTO1
    {
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int PurchaseItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string CreatedBy { get; set; }
    }
}
